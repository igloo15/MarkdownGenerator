using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public abstract class AbstractTypePart : IMarkdownItem
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
    }
}
