using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    public class MarkdownMethod : AbstractTypePart
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Method;
        
        public MethodInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string ReturnTypeName => ReturnType.Name;

        public Type ReturnType => InternalItem.ReturnType;

        public bool IsAbstract => InternalItem.IsAbstract;

        public Type BaseDefinition => InternalItem.GetBaseDefinition()?.DeclaringType;

        public bool IsOverriden => BaseDefinition != InternalItem.DeclaringType;

        internal MarkdownMethod(MethodInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalItem);
    }
}