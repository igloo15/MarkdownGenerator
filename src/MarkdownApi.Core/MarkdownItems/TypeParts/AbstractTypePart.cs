using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    public abstract class AbstractTypePart : AbstractMarkdownItem, IInternalMarkdownItem
    {
        public MarkdownType ParentType { get; internal set; }

        public string Namespace => ParentType.Namespace;

        public bool IsStatic { get; protected set; }

        public override MarkdownProject Project => ParentType.NamespaceItem.Project;

    }
}
