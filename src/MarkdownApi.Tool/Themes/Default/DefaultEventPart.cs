using Igloo15.MarkdownGenerator.Models;
using System.Reflection;

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

        public string GetLink(MarkdownableEvent value, MemberInfo from)
        {
            var mb = new MarkdownBuilder();

            if (from == null)
                mb.Link(GetName(value), value.FilePath);
            else
                mb.Link(GetName(value), value.InternalEvent.RelativeLink(from));

            return mb.ToString();
        }

        public string GetName(MarkdownableEvent value)
        {
            throw new System.NotImplementedException();
        }

        public MemberInfo GetReturnOrType(MarkdownableEvent value)
        {
            return value.InternalEvent.EventHandlerType;
            //return Beautifier.BeautifyTypeWithLink(value.InternalEvent.EventHandlerType, value.FilePath);
        }

        public string GetSummary(MarkdownableEvent value)
        {
            return value.Summary;
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