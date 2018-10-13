using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// The abstract Markdown item that implements some base properties and functions for MarkdownItems
    /// </summary>
    public abstract class AbstractMarkdownItem : IMarkdownItem, IInternalMarkdownItem
    {

        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        public abstract MarkdownProject Project { get; }

        /// <summary>
        /// The type of markdown item
        /// </summary>
        public abstract MarkdownItemTypes ItemType { get; }

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        public abstract TypeWrapper TypeInfo { get; }

        /// <summary>
        /// The location of the markdown page for this item
        /// </summary>
        public string Location { get; internal set; }

        /// <summary>
        /// The Filename of the markdown page 
        /// </summary>
        public string FileName { get; internal set; }

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public abstract string Name { get; }

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public abstract string FullName { get; }

        /// <summary>
        /// The summary of the markdown item
        /// </summary>
        public string Summary { get; internal set; }

        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        public abstract string BuildPage(ITheme theme);

        void IInternalMarkdownItem.SetLocation(string location)
        {
            Location = location;
        }

        void IInternalMarkdownItem.SetFilename(string filename)
        {
            FileName = filename;
        }
    }
}
