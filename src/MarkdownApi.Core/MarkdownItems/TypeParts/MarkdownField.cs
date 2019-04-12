using igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    /// <summary>
    /// The MarkdownField is a field info that is part of  MarkdownType
    /// </summary>
    public class MarkdownField : AbstractTypePart, IMarkdownTypePartValue
    {
        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Field;
        
        /// <summary>
        /// The FieldInfo for this MarkdownField
        /// </summary>
        public FieldInfo InternalItem { get; private set; }

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name => InternalItem.Name;

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public override string FullName => InternalItem.Name;

        /// <summary>
        /// The type name
        /// </summary>
        public string TypeName => InternalItem.FieldType.FullName;
        
        internal MarkdownField(FieldInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
        }

        /// <summary>
        /// The Type the part
        /// </summary>
        public Type Type => InternalItem.FieldType;
        
        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        public override TypeWrapper TypeInfo => new TypeWrapper(InternalItem);
    }
}