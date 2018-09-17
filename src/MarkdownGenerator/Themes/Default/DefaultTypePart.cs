using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultTypePart : IThemePart<MarkdownableType>
    {
        public DefaultTypePart(DefaultTheme defaultTheme)
        {
            RootTheme = defaultTheme;
        }

        public ITheme RootTheme { get; }

        public string GetCode(MarkdownableType value)
        {
            var mb = new MarkdownBuilder();
            var sb = new StringBuilder();
            var InternalType = value.InternalType;


            var stat = (InternalType.IsAbstract && InternalType.IsSealed) ? "static " : "";
            var abst = (InternalType.IsAbstract && !InternalType.IsInterface && !InternalType.IsSealed) ? "abstract " : "";
            var classOrStructOrEnumOrInterface = InternalType.IsInterface ? "interface" : InternalType.IsEnum ? "enum" : InternalType.IsValueType ? "struct" : "class";

            sb.AppendLine($"public {stat}{abst}{classOrStructOrEnumOrInterface} {Beautifier.BeautifyType(InternalType, true)}");
            var impl = string.Join(", ", new[] { InternalType.BaseType }.Concat(InternalType.GetInterfaces()).Where(x => x != null && x != typeof(object) && x != typeof(ValueType)).Select(x => Beautifier.BeautifyType(x)));
            if (impl != "")
            {
                sb.AppendLine("    : " + impl);
            }

            mb.Code("csharp", sb.ToString());

            return mb.ToString();
        }

        public string GetDetailed(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetExample(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableType value)
        {
            return value.GenerateTypeRelativeLinkPath(value.InternalType);
        }

        public string GetName(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableType value)
        {
            var mb = new MarkdownBuilder();

            mb.HeaderWithCode(1, value.GenerateTypeRelativeLinkPath(value.InternalType));
            mb.AppendLine();

            var comments = value.Comments;

            var desc = comments.FirstOrDefault(x => x.MemberType == MemberType.Type)?.Summary ?? "";

            if (!String.IsNullOrEmpty(desc))
            {
                mb.AppendLine(desc);
            }

            mb.Append(GetCode(value));


            mb.AppendLine();

            BuildTable(mb, "Fields", comments, value.Fields, RootTheme.FieldPart.GetTableHeaders());
            BuildTable(mb, "Properties", comments, value.Properties, new[] { "Type", "Name", "Summary" });

            //1 -Type 2-Name 3-finalName
            //BuildTable(mb, "Fields", value.Fields, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            //BuildTable(mb, "Properties", value.Properties, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            //BuildTable(mb, "Events", value.Events, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            //BuildTable(mb, "Methods", value.Methods, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
            //BuildTable(mb, "Static Fields", value.StaticFields, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalField.FieldType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            //BuildTable(mb, "Static Properties", value.StaticProperties, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalProperty.PropertyType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            //BuildTable(mb, "Static Methods", value.StaticMethods, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalMethod.ReturnType, GenerateTypeRelativeLinkPath), x => x.Name, x => Beautifier.ToMarkdownMethodInfo(x.InternalMethod, GenerateTypeRelativeLinkPath));
            //BuildTable(mb, "Static Events", value.StaticEvents, comments, x => Beautifier.BeautifyTypeWithLink(x.InternalEvent.EventHandlerType, GenerateTypeRelativeLinkPath), x => x.Name, x => x.Name);
            

            return mb.ToString();
        }

        public string GetReturnOrType(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetSummary(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            throw new System.NotImplementedException();
        }

        

        private void BuildTable(MarkdownBuilder mb, string label, IEnumerable<XmlDocumentComment> comments, IMarkdownable[] items, string[] headers)
        {
            if (items.Any())
            {
                mb.Header(2, label);
                mb.AppendLine();


                var seq = items.OrderBy(x => x.Name);

                var data = seq.Select(item2 =>
                {
                    var summary = comments.FirstOrDefault(x => x.MemberName == item2.GetName()
                        || x.MemberName.StartsWith(item2.GetName() + "`"))?.Summary ?? "";

                    return new[] {
                        item2.GetReturnOrType(),
                        item2.GetDetailed(),
                        summary
                    };
                });

                mb.Table(headers, data);
                mb.AppendLine();
            }
        }
    }
}