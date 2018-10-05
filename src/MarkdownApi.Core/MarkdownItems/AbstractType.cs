using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public abstract class AbstractType : AbstractMarkdownItem
    {
        public override string Name => InternalType.Name;

        public override string FullName => InternalType.FullName;

        public MarkdownNamespace NamespaceItem { get; internal set; }

        public Type InternalType { get; internal set; }

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalType);

        public string Namespace => NamespaceItem.FullName;

        public List<string> Interfaces => InternalType.GetInterfaces().Select(t => t.FullName).ToList();

        public bool IsInterface => InternalType.IsInterface;

        public bool IsAbstract => InternalType.IsAbstract;

        public bool IsGeneric => InternalType.ContainsGenericParameters;

        public override MarkdownProject Project => NamespaceItem.Project;

        public IEnumerable<XmlDocumentComment> Comments { get; internal set; }

    }
}
