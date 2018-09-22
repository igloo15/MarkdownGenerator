using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
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

        public string GetLink(MarkdownableType value, MemberInfo from)
        {
            var mb = new MarkdownBuilder();

            if (from == null)
                mb.Link(GetName(value), value.FilePath);
            else
                mb.Link(GetName(value), value.InternalType.RelativeLink(from));

            return mb.ToString();
        }

        public string GetName(MarkdownableType value)
        {
            return value.Name;
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

            BuildTable(mb, "Fields", value.Fields, RootTheme.FieldPart.GetTableHeaders(), value);
            BuildTable(mb, "Properties", value.Properties, RootTheme.PropertyPart.GetTableHeaders(), value);
            BuildTable(mb, "Methods", value.Methods, RootTheme.MethodPart.GetTableHeaders(), value);

            BuildTable(mb, "Static Fields", value.StaticFields, RootTheme.StaticFieldPart.GetTableHeaders(), value);
            BuildTable(mb, "Static Properties", value.StaticProperties, RootTheme.StaticPropertyPart.GetTableHeaders(), value);
            BuildTable(mb, "Static Methods", value.StaticMethods, RootTheme.StaticMethodPart.GetTableHeaders(), value);

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

        public MemberInfo GetReturnOrType(MarkdownableType value)
        {
            return value.InternalType;
        }

        public string GetSummary(MarkdownableType value)
        {
            return value.Summary;
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Type", "Name", "Summary" };
        }

        

        private void BuildTable(MarkdownBuilder mb, string label, IMarkdownable[] items, string[] headers, MarkdownableType mdType)
        {
            if (items.Any())
            {
                mb.Header(2, label);
                mb.AppendLine();


                var seq = items.OrderBy(x => x.Name);

                var data = seq.Select(item =>
                {
                    return new[] {
                        $"[{item.GetReturnOrType().Name}]({item.GetReturnOrType().RelativeLink(mdType.InternalType)})",
                        item.GetDetailed(),
                        item.GetSummary()
                    };
                });

                mb.Table(headers, data);
                mb.AppendLine();
            }
        }
    }
}