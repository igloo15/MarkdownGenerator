using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public class MarkdownEnum : AbstractMarkdownItem, IInternalMarkdownItem
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Enum;
                
        public override string Name => InternalType.Name;

        public override string FullName => InternalType.FullName;

        public MarkdownNamespace NamespaceItem { get; internal set; }

        public Type InternalType { get; internal set; }

        public string Namespace => NamespaceItem.FullName;

        public List<string> Interfaces => InternalType.GetInterfaces().Select(t => t.FullName).ToList();

        public bool IsInterface => InternalType.IsInterface;

        public bool IsAbstract => InternalType.IsAbstract;

        public bool IsGeneric => InternalType.ContainsGenericParameters;

        public IEnumerable<XmlDocumentComment> Comments { get; internal set; }

        public List<string> EnumNames => InternalType.GetEnumNames().ToList();
        
        public override string BuildPage(ITheme theme)
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
        
        public override MarkdownProject Project => NamespaceItem.Project;

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalType);
    }
}