using System;
using System.Collections.Generic;
using System.Linq;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;

namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownType : IMarkdownItem, IInternalMarkdownItem
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

        public MarkdownField[] GetFields(bool isStatic)
        {
            return Fields.Where(f => f.IsStatic == isStatic).ToArray();
        }

        public MarkdownProperty[] GetProperties(bool isStatic)
        {
            return Properties.Where(f => f.IsStatic == isStatic).ToArray();
        }

        public MarkdownMethod[] GetMethods(bool isStatic)
        {
            return Methods.Where(f => f.IsStatic == isStatic).ToArray();
        }

        public MarkdownEvent[] GetEvents(bool isStatic)
        {
            return Events.Where(f => f.IsStatic == isStatic).ToArray();
        }

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