using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// AbstractType implements base functionality for MarkdownType and MarkdownEnum
    /// </summary>
    public abstract class AbstractType : AbstractMarkdownItem
    {
        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name => InternalType.Name;

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public override string FullName => InternalType.FullName;

        /// <summary>
        /// The name space this markdown is in
        /// </summary>
        public MarkdownNamespace NamespaceItem { get; internal set; }

        /// <summary>
        /// The type information for this MarkdownType
        /// </summary>
        public Type InternalType { get; internal set; }

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        public override TypeWrapper TypeInfo => new TypeWrapper(InternalType);

        /// <summary>
        /// The namespace this item exists in
        /// </summary>
        public string Namespace => NamespaceItem.FullName;

        /// <summary>
        /// THe interfaces this MarkdownType implements
        /// </summary>
        public List<string> Interfaces => InternalType.GetInterfaces().Select(t => t.FullName).ToList();

        /// <summary>
        /// Determines if type is interface
        /// </summary>
        public bool IsInterface => InternalType.IsInterface;

        /// <summary>
        /// Determines if type is abstract
        /// </summary>
        public bool IsAbstract => InternalType.IsAbstract;

        /// <summary>
        /// Determines if Type is Generic
        /// </summary>
        public bool IsGeneric => InternalType.ContainsGenericParameters;

        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        public override MarkdownProject Project => NamespaceItem.Project;

        /// <summary>
        /// The comments related to Type and TypeParts
        /// </summary>
        public IEnumerable<XmlDocumentComment> Comments { get; internal set; }

    }
}
