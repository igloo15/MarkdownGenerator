using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownEnum : IMarkdownItem, IInternalMarkdownItem
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

        public IEnumerable<XmlDocumentComment> Comments { get; internal set; }

        public List<string> EnumNames => InternalType.GetEnumNames().ToList();

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