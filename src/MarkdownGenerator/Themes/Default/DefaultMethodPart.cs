using Igloo15.MarkdownGenerator.Models;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultMethodPart : IThemePart<MarkdownableMethod>
    {
        private DefaultTheme defaultTheme;

        public DefaultMethodPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => defaultTheme;

        public string GetCode(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetExample(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturn(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string GetSummary(MarkdownableMethod value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            throw new System.NotImplementedException();
        }
    }
}