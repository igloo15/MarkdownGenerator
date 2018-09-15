using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace MarkdownWikiGenerator
{
    public class MarkdownableType
    {
        readonly Type type;
        readonly ILookup<string, XmlDocumentComment> commentLookup;

        public string Namespace => type.Namespace;
        public string Name => type.Name;
        public string BeautifyName => Beautifier.BeautifyTypeWithLink(type,GenerateTypeRelativeLinkPath);

        public MarkdownableType(Type type, ILookup<string, XmlDocumentComment> commentLookup)
        {
            this.type = type;
            this.commentLookup = commentLookup;
        }

        MethodInfo[] GetMethods()
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any() && !x.IsPrivate)
                .ToArray();
        }

        PropertyInfo[] GetProperties()
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any())
                .Where(y =>
                {
                    var get = y.GetGetMethod(true);
                    var set = y.GetSetMethod(true);
                    if (get != null && set != null)
                    {
                        return !(get.IsPrivate && set.IsPrivate);
                    }
                    else if (get != null)
                    {
                        return !get.IsPrivate;
                    }
                    else if (set != null)
                    {
                        return !set.IsPrivate;
                    }
                    else
                    {
                        return false;
                    }
                })
                .ToArray();
        }

        FieldInfo[] GetFields()
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any() && !x.IsPrivate)
                .ToArray();
        }

        EventInfo[] GetEvents()
        {
            return type.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any())
                .ToArray();
        }

        FieldInfo[] GetStaticFields()
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any() && !x.IsPrivate)
                .ToArray();
        }

        PropertyInfo[] GetStaticProperties()
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any())
                .Where(y =>
                {
                    var get = y.GetGetMethod(true);
                    var set = y.GetSetMethod(true);
                    if (get != null && set != null)
                    {
                        return !(get.IsPrivate && set.IsPrivate);
                    }
                    else if (get != null)
                    {
                        return !get.IsPrivate;
                    }
                    else if (set != null)
                    {
                        return !set.IsPrivate;
                    }
                    else
                    {
                        return false;
                    }
                })
                .ToArray();
        }

        MethodInfo[] GetStaticMethods()
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any() && !x.IsPrivate)
                .ToArray();
        }

        EventInfo[] GetStaticEvents()
        {
            return type.GetEvents(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && !x.GetCustomAttributes<ObsoleteAttribute>().Any())
                .ToArray();
        }
        void BuildTable<T>(MarkdownBuilder mb, string label, T[] array, IEnumerable<XmlDocumentComment> docs, Func<T, string> type, Func<T, string> name, Func<T, string> finalName)
        {
            if (array.Any())
            {
                mb.AppendLine($"##\t{label}" );
                mb.AppendLine();

                string[] head = (this.type.IsEnum)
                    ? new[] { "Value", "Name", "Summary" }
                    : new[] { "Type", "Name", "Summary" };

                IEnumerable<T> seq = array;
                if (!this.type.IsEnum)
                {
                    seq = array.OrderBy(x => name(x));
                }

                var data = seq.Select(item2 =>
                {
                    var summary = docs.FirstOrDefault(x => x.MemberName == name(item2) 
                    || x.MemberName.StartsWith(name(item2) + "`"))?.Summary ?? "";
                    return new[] {
                        //MarkdownBuilder.MarkdownCodeQuote(),
                        type(item2),
                        finalName(item2),
                        summary };
                });

                mb.Table(head, data);
                mb.AppendLine();
            }
        }

        public override string ToString()
        {
            var mb = new MarkdownBuilder();

            mb.HeaderWithCode(1, Beautifier.BeautifyTypeWithLink(type, GenerateTypeRelativeLinkPath, false));
            mb.AppendLine();

            var desc = commentLookup[type.FullName].FirstOrDefault(x => x.MemberType == MemberType.Type)?.Summary ?? "";
            if (desc != "") {
                mb.AppendLine(desc);
            }
            {
                var sb = new StringBuilder();

                var stat = (type.IsAbstract && type.IsSealed) ? "static " : "";
                var abst = (type.IsAbstract && !type.IsInterface && !type.IsSealed) ? "abstract " : "";
                var classOrStructOrEnumOrInterface = type.IsInterface ? "interface" : type.IsEnum ? "enum" : type.IsValueType ? "struct" : "class";

                sb.AppendLine($"public {stat}{abst}{classOrStructOrEnumOrInterface} {Beautifier.BeautifyType(type, true)}");
                var impl = string.Join(", ", new[] { type.BaseType }.Concat(type.GetInterfaces()).Where(x => x != null && x != typeof(object) && x != typeof(ValueType)).Select(x => Beautifier.BeautifyType(x)));
                if (impl != "")
                {
                    sb.AppendLine("    : " + impl);
                }

                mb.Code("csharp", sb.ToString());
            }

            mb.AppendLine();

            if (type.IsEnum)
            {
                var enums = Enum.GetNames(type)
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value =x })
                    .OrderBy(x => x.Value)
                    .ToArray();

                BuildTable(mb, "Enum", enums, commentLookup[type.FullName], x => x.Value, x => x.Name, x => x.Name);
            }
            else
            {
                BuildTable(mb, "Fields", GetFields(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Properties", GetProperties(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Events", GetEvents(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Methods", GetMethods(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.ReturnType, GenerateTypeRelativeLinkPath) , x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Fields", GetStaticFields(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Properties", GetStaticProperties(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Methods", GetStaticMethods(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Events", GetStaticEvents(), commentLookup[type.FullName], x => Beautifier.BeautifyTypeWithLink(x.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            }

            return mb.ToString();
        }

        string GenerateTypeRelativeLinkPath(Type type)
        {
            if (type.Name == "void")
                return string.Empty;
            if (type.Name == "String")
                return string.Empty;
            if (type.Namespace.StartsWith("System"))
                return string.Empty;
            var localNamescape = this.Namespace;
            var linkNamescape = type.Namespace;
            var RelativeLinkPath = $"{(string.Join("/", localNamescape.Split('.').Select(a => "..")))}/{linkNamescape.Replace('.', '/')}/{type.Name}.md";
            return RelativeLinkPath;
        }

        public void GenerateMethodDocuments(string namescapeDirectoryPath)
        {
            var methods = GetMethods();
            var comments = commentLookup[type.FullName];

            foreach (var method in methods)
            {
                var sb = new StringBuilder();

                string generateTypeRelativeLinkPath(Type type){
                    var RelativeLinkPath = $"{(string.Join("/", this.Namespace.Split('.').Select(a => "..")))}/../{type.Namespace.Replace('.', '/')}/{type.Name}.md";
                    return RelativeLinkPath;
                }
                var isExtension = method.GetCustomAttributes<System.Runtime.CompilerServices.ExtensionAttribute>(false).Any();
                var seq = method.GetParameters().Select(x =>
                {
                    var suffix = x.HasDefaultValue ? (" = " + (x.DefaultValue ?? $"null")) : "";
                    return $"{Beautifier.BeautifyTypeWithLink(x.ParameterType, generateTypeRelativeLinkPath)} " + x.Name + suffix;
                });
                sb.AppendLine($"#\t{method.DeclaringType.Name}.{method.Name} Method ({(isExtension ? "this " : "")}{string.Join(", ", seq)})");

                var parameters = method.GetParameters();

                var comment = comments.FirstOrDefault(a => 
                (a.MemberName == method.Name ||
                a.MemberName.StartsWith(method.Name + "`"))
                &&
                parameters.All(b=> a.Parameters.ContainsKey( b.Name)  )
                );

                if (comment != null)
                {

                    if (comment.Parameters != null && comment.Parameters.Count > 0)
                    {
                        sb.AppendLine($"");
                        sb.AppendLine("##\tParameters");


                        foreach (var parameter in parameters)
                        {
                            sb.AppendLine($"");
                            sb.AppendLine($"###\t{parameter.Name}");
                            sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(parameter.ParameterType, generateTypeRelativeLinkPath)}");
                            if (comment.Parameters.ContainsKey(parameter.Name))
                                sb.AppendLine($"-\t{comment.Parameters[parameter.Name]}");
                        }
                    }
                    if (!string.IsNullOrEmpty(comment.Returns))
                    {
                        sb.AppendLine($"");
                        sb.AppendLine("##\tReturn Value");
                        sb.AppendLine($"-\tType: {Beautifier.BeautifyTypeWithLink(method.ReturnType, generateTypeRelativeLinkPath)}");
                        sb.AppendLine($"-\t{comment.Returns}");
                    }

                    sb.AppendLine($"");
                    sb.AppendLine("##\tRemarks");
                    sb.AppendLine($"-\t{comment.Summary}");

                }
                if (!Directory.Exists(Path.Combine(namescapeDirectoryPath, $"{method.DeclaringType.Name}")))
                    Directory.CreateDirectory(Path.Combine(namescapeDirectoryPath, $"{method.DeclaringType.Name}"));

                File.WriteAllText(Path.Combine(namescapeDirectoryPath, $"{method.DeclaringType.Name}/{method.MetadataToken}.md"), sb.ToString());
            }

        }
    }


    public static class MarkdownGenerator
    {
        public static MarkdownableType[] Load(string dllPath, string namespaceMatch)
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
                    var types = x.GetTypes(); ;
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
                MarkdownableType markdownableType = new MarkdownableType(x, commentsLookup);
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
