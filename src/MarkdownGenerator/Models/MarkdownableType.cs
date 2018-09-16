using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableType : IMarkdownable
    {
        public Type InnerType { get; private set; }

        readonly ILookup<string, XmlDocumentComment> commentLookup;

        public string Namespace => InnerType.Namespace;
        public string Name => InnerType.Name;
        public string FullName => InnerType.FullName;
        public bool IsStatic => false;

        public string BeautifyName => Beautifier.BeautifyTypeWithLink(InnerType, GenerateTypeRelativeLinkPath);

        public MarkdownableType(Type type, ILookup<string, XmlDocumentComment> commentLookup)
        {
            InnerType = type;
            this.commentLookup = commentLookup;
        }

        void BuildTable<T>(MarkdownBuilder mb, string label, T[] array, IEnumerable<XmlDocumentComment> docs, Func<T, string> type, Func<T, string> name, Func<T, string> finalName)
        {
            if (array.Any())
            {
                mb.AppendLine($"##\t{label}");
                mb.AppendLine();

                string[] head = (InnerType.IsEnum)
                    ? new[] { "Value", "Name", "Summary" }
                    : new[] { "Type", "Name", "Summary" };

                IEnumerable<T> seq = array;
                if (!InnerType.IsEnum)
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

            mb.HeaderWithCode(1, Beautifier.BeautifyTypeWithLink(InnerType, GenerateTypeRelativeLinkPath, false));
            mb.AppendLine();

            var desc = commentLookup[InnerType.FullName].FirstOrDefault(x => x.MemberType == MemberType.Type)?.Summary ?? "";
            if (desc != "")
            {
                mb.AppendLine(desc);
            }
            {
                var sb = new StringBuilder();

                var stat = (InnerType.IsAbstract && InnerType.IsSealed) ? "static " : "";
                var abst = (InnerType.IsAbstract && !InnerType.IsInterface && !InnerType.IsSealed) ? "abstract " : "";
                var classOrStructOrEnumOrInterface = InnerType.IsInterface ? "interface" : InnerType.IsEnum ? "enum" : InnerType.IsValueType ? "struct" : "class";

                sb.AppendLine($"public {stat}{abst}{classOrStructOrEnumOrInterface} {Beautifier.BeautifyType(InnerType, true)}");
                var impl = string.Join(", ", new[] { InnerType.BaseType }.Concat(InnerType.GetInterfaces()).Where(x => x != null && x != typeof(object) && x != typeof(ValueType)).Select(x => Beautifier.BeautifyType(x)));
                if (impl != "")
                {
                    sb.AppendLine("    : " + impl);
                }

                mb.Code("csharp", sb.ToString());
            }

            mb.AppendLine();

            if (InnerType.IsEnum)
            {
                var enums = Enum.GetNames(InnerType)
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value = x
                    })
                    .OrderBy(x => x.Value)
                    .ToArray();

                BuildTable(mb, "Enum", enums, commentLookup[InnerType.FullName], x => x.Value, x => x.Name, x => x.Name);
            }
            else
            {
                BuildTable(mb, "Fields", this.GetFields(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Properties", this.GetProperties(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Events", this.GetEvents(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Methods", this.GetMethods(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Fields", this.GetStaticFields(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Properties", this.GetStaticProperties(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
                BuildTable(mb, "Static Methods", this.GetStaticMethods(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
                BuildTable(mb, "Static Events", this.GetStaticEvents(), commentLookup[InnerType.FullName], x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
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
            var methods = this.GetMethods();
            var comments = commentLookup[FullName];

            foreach (var method in methods.Select(i => i.InternalMethod))
            {
                var sb = new StringBuilder();

                string generateTypeRelativeLinkPath(Type type)
                {
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
                parameters.All(b => a.Parameters.ContainsKey(b.Name))
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
}
