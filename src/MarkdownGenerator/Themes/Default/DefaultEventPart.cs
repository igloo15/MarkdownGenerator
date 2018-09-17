using Igloo15.MarkdownGenerator.Models;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultEventPart : IThemePart<MarkdownableEvent>
    {
        private DefaultTheme defaultTheme;

        public DefaultEventPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => throw new System.NotImplementedException();

        public string GetCode(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetExample(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturnOrType(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string GetSummary(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }
    }
}