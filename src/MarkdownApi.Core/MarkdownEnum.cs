using System;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownEnum : IMarkdownItem
    {
        public MarkdownItemTypes ItemType => MarkdownItemTypes.Enum;

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

        public string Summary { get; internal set; }

        public List<string> EnumNames => InternalType.GetEnumNames().ToList();

    }
}