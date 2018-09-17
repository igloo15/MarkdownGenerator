using Igloo15.MarkdownGenerator.Models;

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
            throw new System.NotImplementedException();
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
            throw new System.NotImplementedException();
        }

        public string GetName(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetPage(MarkdownableType value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturn(MarkdownableType value)
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
    }
}