using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownField : AbstractTypePart
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Field;
        
        public FieldInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string TypeName => InternalItem.FieldType.FullName;
        
        public MarkdownField(FieldInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }
    }
}