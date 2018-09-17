using Igloo15.MarkdownGenerator.Models;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultNamespacePart : IThemePart<MarkdownableNamespace>
    {
        private DefaultTheme defaultTheme;

        public DefaultNamespacePart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => defaultTheme;

        public string GetCode(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetExample(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturn(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string GetSummary(MarkdownableNamespace value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            throw new System.NotImplementedException();
        }
    }
}