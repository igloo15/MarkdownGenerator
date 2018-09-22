using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultEnumPart : IThemePart<MarkdownableType>
    {
        private DefaultTheme defaultTheme;

        public DefaultEnumPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => defaultTheme;

        public string GetCode(MarkdownableType value)
        {
            return "ENUM CODE";
        }

        public string GetDetailed(MarkdownableType value)
        {
            return value.Name;
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

        public MemberInfo GetReturnOrType(MarkdownableType value)
        {
            return value.InternalType;
            //return Beautifier.BeautifyTypeWithLink(value.InternalType, value.FilePath);
        }

        public string GetSummary(MarkdownableType value)
        {
            return value.Summary;
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Type", "Name", "Summary" };
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

            BuildEnumTable(mb, comments, value);

            return mb.ToString();
        }

        private void BuildEnumTable(MarkdownBuilder mb, IEnumerable<XmlDocumentComment> comments, MarkdownableType value)
        {
            
            var enums = Enum.GetNames(value.InternalType)
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value = x
                    })
                    .OrderBy(x => x.Value)
                    .ToArray();

            if (enums.Any())
            {
                mb.AppendLine($"##\tEnum");
                mb.AppendLine();

                string[] head = new[] { "Value", "Name", "Summary" };

                var data = enums.Select(item =>
                {
                    var summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";


                    return new[] {
                        item.Value,
                        item.Name,
                        summary
                    };
                });

                mb.Table(head, data);
                mb.AppendLine();
            }
        }
    }
}