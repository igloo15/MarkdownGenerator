using Igloo15.MarkdownGenerator.Models;

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

        public string GetLink(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturnOrType(MarkdownableField value)
        {
            return Beautifier.BeautifyTypeWithLink(value.InternalField.FieldType, value.FilePath);
        }

        public string GetSummary(MarkdownableField value)
        {
            throw new System.NotImplementedException();
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