using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownProperty : AbstractTypePart
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Property;

        public PropertyInfo InternalItem { get; internal set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string TypeName => InternalItem.PropertyType.FullName;
        
        public MarkdownProperty(PropertyInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }
    }
}