
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Igloo15.MarkdownApi.Core.Builders
{
    
    internal static class MarkdownItemBuilder
    {
        public static MarkdownProject Load(string searchArea, string namespaceMatch)
        {
            List<MarkdownType> types = new List<MarkdownType>();

            MarkdownProject project = new MarkdownProject();

            var dllPaths = searchArea.Split(';');
            foreach(var dllPath in dllPaths)
            {
                Constants.Logger?.LogInformation("Searching {searchArea} for dlls", dllPath);

                
                var index = dllPath.LastIndexOf(Path.DirectorySeparatorChar);
                if (index < 0)
                {
                    Constants.Logger?.LogWarning($"SKIPPING : Index {index} found for path {dllPath}");
                    continue;
                }
                    

                var directoryPath = dllPath.Substring(0, index);
                var filePath = dllPath.Substring(index+1);

                DirectoryInfo folder = new DirectoryInfo(directoryPath);
                if (folder.Exists) // else: Invalid folder!
                {
                    FileInfo[] files = folder.GetFiles(filePath);

                    Constants.Logger?.LogInformation("Found {dllCount} dlls in {folderPath}", files.Length, directoryPath);

                    foreach (FileInfo file in files)
                    {
                        Constants.Logger?.LogInformation("Loading Dll: {dllName}", file.Name);
                        try
                        {
                            LoadDll(file.FullName, namespaceMatch);
                        }
                        catch(Exception e)
                        {
                            Constants.Logger?.LogError(e, "Failed to Load {dllName}", file.FullName);
                        }
                    }
                }
                else
                {
                    Constants.Logger?.LogWarning("{folderPath} folder does not exist", directoryPath);
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

            IEnumerable<Type> AssemblyTypesSelector(Assembly x) {

                try
                {
                    var types = x.GetTypes();
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

            bool NotNullPredicate(Type x )
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

            var dllAssemblys = new[] { Assembly.LoadFrom(dllPath) };

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

        static bool IsRequiredNamespace(Type type, Regex regex) {
            if ( regex == null ) {
                return true;
            }
            return regex.IsMatch(type.Namespace != null ? type.Namespace : string.Empty);
        }

        static ILookup<string, XmlDocumentComment> GetComments(string dllPath, string namespaceMatch)
        {
            var dllName = Path.GetFileNameWithoutExtension(dllPath);
            var xmlPath = Path.Combine(Directory.GetParent(dllPath).FullName, dllName + ".xml");

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
