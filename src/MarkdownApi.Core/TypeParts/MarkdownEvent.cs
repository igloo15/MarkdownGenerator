using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownEvent : AbstractTypePart 
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
    }
}