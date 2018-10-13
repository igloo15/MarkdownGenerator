using System;
using System.Collections.Generic;
using System.Linq;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// A markdown type is a markdownitem containing a Type
    /// </summary>
    public class MarkdownType : AbstractType
    {
        
        internal MarkdownType() { }

        internal List<MarkdownProperty> Properties { get; } = new List<MarkdownProperty>();

        internal List<MarkdownField> Fields { get; } = new List<MarkdownField>();

        internal List<MarkdownEvent> Events { get; } = new List<MarkdownEvent>();

        internal List<MarkdownMethod> Methods { get; } = new List<MarkdownMethod>();

        internal List<MarkdownConstructor> Constructors { get; } = new List<MarkdownConstructor>();


        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Type;

        /// <summary>
        /// The Generic Properties for this MarkdownType
        /// </summary>
        public List<MarkdownType> GenericProperties { get; } = new List<MarkdownType>();

        /// <summary>
        /// Gets the MarkdownFields in this type
        /// </summary>
        /// <param name="isStatic">determins if the fields returned are static or not</param>
        /// <returns>An array of MarkdownFields</returns>
        public MarkdownField[] GetFields(bool isStatic) => Fields.Where(f => f.IsStatic == isStatic).ToArray();

        /// <summary>
        /// Gets the MarkdownProperties in this type
        /// </summary>
        /// <param name="isStatic">determines if the properties are static or not</param>
        /// <returns>An array of MarkdownProperties</returns>
        public MarkdownProperty[] GetProperties(bool isStatic) => Properties.Where(f => f.IsStatic == isStatic).ToArray();

        /// <summary>
        /// Gets the MarkdownMethods in this type
        /// </summary>
        /// <param name="isStatic">determines if the methods are static or not</param>
        /// <returns>An array of MarkdownMethods</returns>
        public MarkdownMethod[] GetMethods(bool isStatic) => Methods.Where(f => f.IsStatic == isStatic).ToArray();

        /// <summary>
        /// Gets the MarkdownEvents in this type
        /// </summary>
        /// <param name="isStatic">determines if the events are static or not</param>
        /// <returns>An array of MarkdownEvents</returns>
        public MarkdownEvent[] GetEvents(bool isStatic) => Events.Where(f => f.IsStatic == isStatic).ToArray();

        /// <summary>
        /// Gets the MarkdownConstructors in this type
        /// </summary>
        /// <param name="isStatic">determines if the constructor are static or not</param>
        /// <returns>An Array of MarkdownConstructors</returns>
        public MarkdownConstructor[] GetConstructors(bool isStatic) => Constructors.Where(c => c.IsStatic == isStatic).ToArray();

        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        public override string BuildPage(ITheme theme) => theme.BuildPage(this);

    }
}