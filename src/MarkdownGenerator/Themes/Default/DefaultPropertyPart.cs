using Igloo15.MarkdownGenerator.Models;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultPropertyPart : IThemePart<MarkdownableProperty>
    {
        private DefaultTheme defaultTheme;

        public DefaultPropertyPart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => throw new System.NotImplementedException();

        public string GetName(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }

        public string GetLink(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }

        public string GetReturnOrType(MarkdownableProperty value)
        {
            return Beautifier.BeautifyTypeWithLink(value.InternalProperty.PropertyType, value.FilePath);
        }

        public string GetSummary(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }

        public string GetCode(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }

        public string GetDetailed(MarkdownableProperty value)
        {
            return value.Name;
        }

        public string GetExample(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }

        public string[] GetTableHeaders()
        {
            return new[] { "Type", "Name", "Summary" };
        }

        public string GetPage(MarkdownableProperty value)
        {
            throw new System.NotImplementedException();
        }
    }
}