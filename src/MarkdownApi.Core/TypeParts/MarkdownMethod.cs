using System.Reflection;

namespace Igloo15.MarkdownApi.Core.TypeParts
{
    public class MarkdownMethod : AbstractTypePart
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Method;
        
        public MethodInfo InternalItem { get; private set; }

        public override string Name => InternalItem.Name;

        public override string FullName => InternalItem.Name;

        public string ReturnType => InternalItem.ReturnType.Name;
        
        public MarkdownMethod(MethodInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }
    }
}