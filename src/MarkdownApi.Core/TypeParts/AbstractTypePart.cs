using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public abstract class AbstractTypePart : IMarkdownItem, IInternalMarkdownItem
    {
        public string Location { get; internal set; }

        public string FileName { get; internal set; }

        public MarkdownType ParentType { get; internal set; }

        public string Namespace => ParentType.Namespace;

        public bool IsStatic { get; protected set; }

        public string Summary { get; internal set; }

        public abstract MarkdownItemTypes ItemType { get; }

        public abstract string Name { get; }

        public abstract string FullName { get; }

        void IInternalMarkdownItem.SetLocation(string location)
        {
            Location = location;
        }

        void IInternalMarkdownItem.SetFilename(string filename)
        {
            FileName = filename;
        }

        public abstract string GetId();

        public abstract string BuildPage(ITheme theme);

        public Dictionary<string, IMarkdownItem> AllItems => ParentType.NamespaceItem.Project.AllItems;
    }
}
