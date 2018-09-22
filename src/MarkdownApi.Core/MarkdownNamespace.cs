using System.Collections.Generic;
using System.Linq;


namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownNamespace : IMarkdownItem
    {
        public string Location { get; internal set; }

        public string FileName { get; internal set; }

        public string Name => FullName.Split('.').Last();

        public string FullName { get; internal set; }

        public MarkdownItemTypes ItemType => MarkdownItemTypes.Namespace;

        public List<MarkdownType> Types { get; } = new List<MarkdownType>();

        public List<MarkdownEnum> Enums { get; } = new List<MarkdownEnum>();

        public MarkdownProject Project { get; internal set; }
    }
}