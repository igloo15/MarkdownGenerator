using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// Markdown Enum is a type that is a Enum
    /// </summary>
    public class MarkdownEnum : AbstractType
    {
        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Enum;
        
        /// <summary>
        /// The Enum names in this Markdown Enum
        /// </summary>
        public List<string> EnumNames => InternalType.GetEnumNames().ToList();

        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        public override string BuildPage(ITheme theme) => theme.BuildPage(this);

        internal MarkdownEnum() { }
    }
}