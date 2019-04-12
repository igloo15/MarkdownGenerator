using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using igloo15.MarkdownApi.Core.Interfaces;

namespace igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    /// <summary>
    /// Markdown Constructor is a TypePart identified as the construct of a Markdown Type
    /// </summary>
    public class MarkdownConstructor : AbstractTypePart
    {
        /// <summary>
        /// The ConstructorInfo for this Markdown Constructor
        /// </summary>
        public ConstructorInfo InternalItem { get; private set; }

        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Constructor;

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name => InternalItem.Name;

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public override string FullName => InternalItem.Name;

        internal MarkdownConstructor(ConstructorInfo info, bool isStatic)
        {
            IsStatic = isStatic;
            InternalItem = info;
        }
        
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
