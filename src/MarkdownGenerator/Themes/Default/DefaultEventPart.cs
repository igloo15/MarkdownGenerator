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
            return value.Name;
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
            return Beautifier.BeautifyTypeWithLink(value.InternalEvent.EventHandlerType, value.FilePath);
        }

        public string GetSummary(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Type", "Name", "Summary" };
        }

        public string GetPage(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }
    }
}