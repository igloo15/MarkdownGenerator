using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownMethod : AbstractTypePart
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Method;
        
        public MethodInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string ReturnTypeName => ReturnType.Name;

        public Type ReturnType => InternalItem.ReturnType;
        
        public MarkdownMethod(MethodInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        public override string GetId()
        {
            return $"{ParentType.FullName}-{InternalItem.MetadataToken}";
        }

        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }
    }
}