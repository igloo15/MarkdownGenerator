using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Igloo15.MarkdownGenerator
{
    
    internal static class MarkdownGenerator
    {
        public static MarkdownableProject Load(string searchArea, string namespaceMatch, Options config)
        {
            List<MarkdownableType> types = new List<MarkdownableType>();

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
                        types.AddRange(LoadInternal(file.FullName, namespaceMatch, config));
                    }
                }
            }

            var result = types.GroupBy(x => x.Namespace).OrderBy(x => x.Key).Select(x => new MarkdownableNamespace(x.ToList(), x.Key, config));

            return new MarkdownableProject(config, result.ToArray());
            
        }

        private static MarkdownableType[] LoadInternal(string dllPath, string namespaceMatch, Options config)
        {
            var xmlPath = Path.Combine(Directory.GetParent(dllPath).FullName, Path.GetFileNameWithoutExtension(dllPath) + ".xml");

            XmlDocumentComment[] comments = new XmlDocumentComment[0];
            if (File.Exists(xmlPath))
            {
                comments = VSDocParser.ParseXmlComment(XDocument.Parse(File.ReadAllText(xmlPath)), namespaceMatch);
            }
            var commentsLookup = comments.ToLookup(x => x.ClassName);

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

            MarkdownableType markdownableTypeSelector(Type x)
            {
                MarkdownableType markdownableType = new MarkdownableType(x, commentsLookup, config);
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


            return markdownableTypes;
        }

        static bool IsRequiredNamespace(Type type, Regex regex) {
            if ( regex == null ) {
                return true;
            }
            return regex.IsMatch(type.Namespace != null ? type.Namespace : string.Empty);
        }
    }
}
