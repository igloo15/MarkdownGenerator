using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownEvent : AbstractTypePart, IMarkdownTypePartValue 
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Event;

        public EventInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string TypeName => InternalItem.EventHandlerType.FullName;

        public MarkdownEvent(EventInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        public Type Type => InternalItem.EventHandlerType;

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