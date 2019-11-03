using igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.FileSystemGlobbing;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace igloo15.MarkdownApi.Core.Builders
{

    internal static class MarkdownItemBuilder
    {
        public static string xmlPath;
        public static MarkdownProject Load(string searchArea, string namespaceMatch)
        {
            List<MarkdownType> types = new List<MarkdownType>();

            MarkdownProject project = new MarkdownProject();

            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            var matcher = new Matcher();
            foreach (var searchPath in searchArea.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries))
            {
                matcher.AddInclude(searchPath);
            }
            var rootDir = Directory.GetCurrentDirectory();
            var results = matcher.GetResultsInFullPath(rootDir);
            Constants.Logger?.LogInformation($"Found {results.Count()} dlls from {rootDir} using {searchArea}");
            List<string> processedDlls = new List<string>();
            foreach (var dllPath in results)
            {
                var dllName = Path.GetFileName(dllPath);
                Constants.Logger?.LogInformation("Loading Dll: {dllName}", dllPath);

                if (processedDlls.Contains(dllName))
                {
                    Constants.Logger?.LogWarning("Duplicate Dll: {dllName}", dllName);
                    continue;
                }

                try
                {
                    if (File.Exists(dllPath) && Path.GetExtension(dllPath) == ".dll")
                    {
                        LoadDll(dllPath, namespaceMatch);
                        processedDlls.Add(dllName);
                    }
                    else
                    {
                        Constants.Logger?.LogError($"Failed to find {dllPath} dll");
                    }
                }
                catch (Exception e)
                {
                    Constants.Logger?.LogError(e, "Failed to Load {dllName}", dllPath);
                }

            }

            return project;
        }

        private static MarkdownNamespace[] LoadDll(string dllPath, string namespaceMatch)
        {
            var dllName = Path.GetFileNameWithoutExtension(dllPath);
            var commentsLookup = GetComments(dllPath, namespaceMatch);

            var namespaceRegex =
                !string.IsNullOrEmpty(namespaceMatch) ? new Regex(namespaceMatch) : null;

            IEnumerable<Type> AssemblyTypesSelector(Assembly x)
            {

                try
                {
                    var types = x.GetExportedTypes();
                    Constants.Logger?.LogDebug("Loaded {typeCount} Types Successfully", types.Count());
                    return types;
                }
                catch (ReflectionTypeLoadException ex)
                {
                    Constants.Logger?.LogWarning("Failed to Load some types in {dllName}", dllName);
                    return ex.Types.Where(t => t != null);
                }
                catch
                {
                    Constants.Logger?.LogWarning("Failed to Load any types in {dllName}", dllName);
                    return Type.EmptyTypes;
                }
            }

            bool NotNullPredicate(Type x)
            {
                return x != null;
            }

            bool NamespaceFilterPredicate(Type x)
            {
                var _IsRequiredNamespace = IsRequiredNamespace(x, namespaceRegex);
                return _IsRequiredNamespace;
            }

            MarkdownType markdownableTypeSelector(Type x)
            {
                MarkdownType markdownableType = new MarkdownType() { InternalType = x };
                return markdownableType;
            }

            bool OthersPredicate(Type x)
            {
                var IsPublic = x.IsPublic;
                var IsAssignableFromDelegate = typeof(Delegate).IsAssignableFrom(x);
                var HaveObsoleteAttribute = x.GetCustomAttributes<ObsoleteAttribute>().Any();
                return IsPublic && !IsAssignableFromDelegate && !HaveObsoleteAttribute;
            }



            var dllAssemblys = new[] { Assembly.LoadFile(dllPath) };

            var markdownableTypes = dllAssemblys
                .SelectMany(AssemblyTypesSelector)
                .Where(NotNullPredicate)
                .Where(OthersPredicate)
                .Where(NamespaceFilterPredicate)
                .Select(markdownableTypeSelector)
                .ToArray();



            Constants.Logger?.LogDebug("Beginning Building of Markdown Items in {dllName}", dllName);

            return new MarkdownTypeBuilder().BuildTypes(markdownableTypes, commentsLookup).ToArray();
        }

        private static Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            // Ignore missing resources
            if (args.Name.Contains(".resources"))
                return null;

            // check for assemblies already loaded
            Assembly assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            // Try to load by filename - split out the filename of the full assembly name
            // and append the base path of the original assembly (ie. look in the same dir)
            string filename = args.Name.Split(',')[0] + ".dll".ToLower();

            var currentPath = Path.GetDirectoryName(args.RequestingAssembly.CodeBase).Replace("file://", "").Replace("file:\\", "");

            string asmFile = Path.Combine(currentPath, filename);

            try
            {
                return Assembly.LoadFrom(asmFile);
            }
            catch (Exception)
            {
                return null;
            }
        }

        static bool IsRequiredNamespace(Type type, Regex regex)
        {
            if (regex == null)
            {
                return true;
            }
            return regex.IsMatch(type.Namespace != null ? type.Namespace : string.Empty);
        }

       static ILookup<string, XmlDocumentComment> GetComments(string dllPath, string namespaceMatch)
        {
            var dllName = Path.GetFileNameWithoutExtension(dllPath);
            xmlPath = Path.Combine(Directory.GetParent(dllPath).FullName, dllName + ".xml");

            XmlDocumentComment[] comments = new XmlDocumentComment[0];

            if (File.Exists(xmlPath))
            {
                comments = VSDocParser.ParseXmlComment(XDocument.Parse(File.ReadAllText(xmlPath)), namespaceMatch);
                Constants.Logger?.LogDebug("Found {commentCount} comments for {dllName}", comments.Count(), dllName);
            }
            else
            {
                Constants.Logger?.LogWarning("No Documentation Xml found for {dllPath}", dllName);
            }
            var commentsLookup = comments.ToLookup(x => x.ClassName);

            return commentsLookup;
        }
    }
}
