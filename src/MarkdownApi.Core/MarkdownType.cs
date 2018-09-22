using System;
using System.Collections.Generic;
using System.Linq;
using Igloo15.MarkdownApi.Core.TypeParts;

namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownType : IMarkdownItem
    {
        public MarkdownItemTypes ItemType => MarkdownItemTypes.Type;

        public string Location { get; internal set; }

        public string FileName { get; internal set; }

        public string Name => InternalType.Name;

        public string FullName => InternalType.FullName;

        public MarkdownNamespace NamespaceItem { get; internal set; }

        public Type InternalType { get; internal set; }

        public string Namespace => NamespaceItem.FullName;

        public List<string> Interfaces => InternalType.GetInterfaces().Select(t => t.FullName).ToList();

        public bool IsInterface => InternalType.IsInterface;

        public bool IsAbstract => InternalType.IsAbstract;

        public bool IsGeneric => InternalType.ContainsGenericParameters;

        public List<MarkdownProperty> Properties { get; } = new List<MarkdownProperty>();

        public List<MarkdownField> Fields { get; } = new List<MarkdownField>();

        public List<MarkdownEvent> Events { get; } = new List<MarkdownEvent>();

        public List<MarkdownMethod> Methods { get; } = new List<MarkdownMethod>();

        public string Summary { get; internal set; }

        public List<MarkdownType> GenericProperties => new List<MarkdownType>();
    }
}