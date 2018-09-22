using Igloo15.MarkdownApi.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownNamespace : IMarkdownItem, IInternalMarkdownItem
    {
        public string Location { get; internal set; }

        public string FileName { get; internal set; }

        public string Name => FullName.Split('.').Last();

        public string FullName { get; internal set; }

        public MarkdownItemTypes ItemType => MarkdownItemTypes.Namespace;

        public List<MarkdownType> Types { get; } = new List<MarkdownType>();

        public List<MarkdownEnum> Enums { get; } = new List<MarkdownEnum>();

        public MarkdownProject Project { get; internal set; }

        public string Summary => "";

        public string GetId()
        {
            return $"{FullName}";
        }

        public string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        void IInternalMarkdownItem.SetLocation(string location)
        {
            Location = location;
        }

        void IInternalMarkdownItem.SetFilename(string filename)
        {
            FileName = filename;
        }
    }
}