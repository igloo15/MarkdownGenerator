using igloo15.MarkdownApi.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;


namespace igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// Markdown Namespace is a single namespace in a project
    /// </summary>
    public class MarkdownNamespace : AbstractMarkdownItem
    {
        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name => FullName.Split('.').Last();

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public override string FullName { get; }

        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Namespace;

        /// <summary>
        /// The types that exist at this Namespace
        /// </summary>
        public List<MarkdownType> Types { get; } = new List<MarkdownType>();

        /// <summary>
        /// The enums that exist at this Namespace
        /// </summary>
        public List<MarkdownEnum> Enums { get; } = new List<MarkdownEnum>();

        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        public override MarkdownProject Project => InternalProject;

        internal MarkdownProject InternalProject { get; set; }

        internal MarkdownNamespace(string fullName)
        {
            FullName = fullName;
            Summary = "";
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
        /// Lookup of all the MarkdownItems in the project
        /// </summary>
        public Dictionary<string, IMarkdownItem> AllItems => Project.AllItems;

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        public override TypeWrapper TypeInfo => new TypeWrapper(FullName);
    }
}