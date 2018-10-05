using Igloo15.MarkdownApi.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public class MarkdownNamespace : AbstractMarkdownItem
    {
        public override string Name => FullName.Split('.').Last();

        public override string FullName { get; }

        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Namespace;

        public List<MarkdownType> Types { get; } = new List<MarkdownType>();

        public List<MarkdownEnum> Enums { get; } = new List<MarkdownEnum>();

        public override MarkdownProject Project => InternalProject;

        internal MarkdownProject InternalProject { get; set; }

        internal MarkdownNamespace(string fullName)
        {
            FullName = fullName;
            Summary = "";
        }

        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }


        public Dictionary<string, IMarkdownItem> AllItems => Project.AllItems;

        public override TypeWrapper TypeInfo => new TypeWrapper(FullName);
    }
}