using igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    /// <summary>
    /// Abstract Markdown Type Part used to implement basic MarkdownTypePart functionality
    /// </summary>
    public abstract class AbstractTypePart : AbstractMarkdownItem, IInternalMarkdownItem
    {
        
        /// <summary>
        /// The parent Markdown Type
        /// </summary>
        public MarkdownType ParentType { get; internal set; }

        /// <summary>
        /// The namespace of the parent type
        /// </summary>
        public string Namespace => ParentType.Namespace;

        /// <summary>
        /// Is Static determines if the Type Part is static
        /// </summary>
        public bool IsStatic { get; protected set; }

        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        public override MarkdownProject Project => ParentType.NamespaceItem.Project;

    }
}
