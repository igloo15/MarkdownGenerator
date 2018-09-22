using Igloo15.MarkdownGenerator.Models;
using System.Reflection;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultFieldPart : IThemePart<MarkdownableField>
    {
        private DefaultTheme defaultTheme;

        public DefaultFieldPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => throw new System.NotImplementedException();

        public string GetCode(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableField value)
        {
            return value.Name;
        }

        public string GetExample(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableField value, MemberInfo from)
        {
            var mb = new MarkdownBuilder();

            if (from == null)
                mb.Link(GetName(value), value.FilePath);
            else
                mb.Link(GetName(value), value.InternalField.RelativeLink(from));

            return mb.ToString();
        }

        public string GetName(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public MemberInfo GetReturnOrType(MarkdownableField value)
        {
            return value.InternalField.FieldType;
            //return Beautifier.BeautifyTypeWithLink(value.InternalField.FieldType, value.FilePath);
        }

        public string GetSummary(MarkdownableField value)
        {
            return value.Summary;
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Type", "Name", "Summary" };
        }

        public string GetPage(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }
    }
}