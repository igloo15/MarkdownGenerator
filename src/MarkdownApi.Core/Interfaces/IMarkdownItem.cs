using Igloo15.MarkdownApi.Core.MarkdownItems;
using System.Collections.Generic;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    /// <summary>
    /// A markdown item that could be parsed to a markdown page
    /// </summary>
    public interface IMarkdownItem
    {
        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        MarkdownProject Project { get; }

        /// <summary>
        /// The type of markdown item
        /// </summary>
        MarkdownItemTypes ItemType { get; }

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        TypeWrapper TypeInfo { get; }

        /// <summary>
        /// The location of the markdown page for this item
        /// </summary>
        string Location { get; }

        /// <summary>
        /// The Filename of the markdown page 
        /// </summary>
        string FileName { get; }

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        string Name { get; }

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        string FullName { get; }

        /// <summary>
        /// The summary of the markdown item
        /// </summary>
        string Summary { get; }

        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        string BuildPage(ITheme theme);
    }
}