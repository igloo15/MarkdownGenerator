using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    public class MarkdownProperty : AbstractTypePart, IMarkdownTypePartValue
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Property;

        public PropertyInfo InternalItem { get; internal set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string TypeName => InternalItem.PropertyType.FullName;
        
        internal MarkdownProperty(PropertyInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        public Type Type => InternalItem.PropertyType;
        
        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalItem);
    }
}