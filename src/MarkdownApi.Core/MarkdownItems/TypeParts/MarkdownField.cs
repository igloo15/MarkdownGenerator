using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    public class MarkdownField : AbstractTypePart, IMarkdownTypePartValue
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Field;
        
        public FieldInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string TypeName => InternalItem.FieldType.FullName;
        
        internal MarkdownField(FieldInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        public Type Type => InternalItem.FieldType;
        
        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalItem);
    }
}