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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public string GetSummary(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableField value)
        {
            throw new System.NotImplementedException();
        }
    }
}