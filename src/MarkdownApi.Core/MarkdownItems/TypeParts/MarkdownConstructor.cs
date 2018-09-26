using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using Igloo15.MarkdownApi.Core.Interfaces;

namespace Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    public class MarkdownConstructor : AbstractTypePart
    {
        public ConstructorInfo InternalItem { get; private set; }

        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Constructor;

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public MarkdownConstructor(ConstructorInfo info, bool isStatic)
        {
            IsStatic = isStatic;
            InternalItem = info;
        }
        
        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        public override TypeWrapper TypeInfo => new TypeWrapper(InternalItem);

    }
}
