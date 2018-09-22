
using Igloo15.MarkdownApi.Core;
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
        public static MarkdownProject Load(string searchArea, string namespaceMatch, string root)
        {
            List<MarkdownType> types = new List<MarkdownType>();

            MarkdownProject project = new MarkdownProject(root);

            var dllPaths = searchArea.Split(';');
            foreach(var dllPath in dllPaths)
            {
                var index = dllPath.LastIndexOf(Path.DirectorySeparatorChar);

                var directoryPath = dllPath.Substring(0, index);
                var filePath = dllPath.Substring(index+1);

                DirectoryInfo folder = new DirectoryInfo(directoryPath);
                if (folder.Exists) // else: Invalid folder!
                {
                    FileInfo[] files = folder.GetFiles(filePath);

                    foreach (FileInfo file in files)
                    {
                        try
                        {
                            project.AddNamespaces(LoadDll(file.FullName, namespaceMatch));
                        }
                        catch(Exception e)
                        {
                            Console.WriteLine($"Failed to load file {file.FullName}");
                        }
                        
                    }
                }
            }

            return project;
        }

        private static MarkdownNamespace[] LoadDll(string dllPath, string namespaceMatch)
        {
            var commentsLookup = GetComments(dllPath, namespaceMatch);

            var namespaceRegex = 
                !string.IsNullOrEmpty(namespaceMatch) ? new Regex(namespaceMatch) : null;

            IEnumerable< Type> AssemblyTypesSelector(Assembly x) {

                try
                {
                    var types = x.GetTypes();
                    return types;
                }
                catch (ReflectionTypeLoadException ex)
                {
                    return ex.Types.Where(t => t != null);
                }
                catch
                {
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
            var xmlPath = Path.Combine(Directory.GetParent(dllPath).FullName, Path.GetFileNameWithoutExtension(dllPath) + ".xml");

            XmlDocumentComment[] comments = new XmlDocumentComment[0];
            if (File.Exists(xmlPath))
            {
                comments = VSDocParser.ParseXmlComment(XDocument.Parse(File.ReadAllText(xmlPath)), namespaceMatch);
            }
            var commentsLookup = comments.ToLookup(x => x.ClassName);

            return commentsLookup;
        }
    }
}
