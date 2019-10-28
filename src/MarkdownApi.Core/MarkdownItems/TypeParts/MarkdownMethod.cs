using igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Reflection;

namespace igloo15.MarkdownApi.Core.MarkdownItems.TypeParts
{
    /// <summary>
    /// MarkdownMethod represents the method info of a MarkdownType
    /// </summary>
    public class MarkdownMethod : AbstractTypePart
    {
        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Method;
        
        /// <summary>
        /// The Method Info for this markdown item
        /// </summary>
        public MethodInfo InternalItem { get; private set; }

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name => InternalItem.Name;

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
        public override string FullName => InternalItem.Name;

        /// <summary>
        /// The name of the return type for this method
        /// </summary>
        public string ReturnTypeName => ReturnType.Name;

        /// <summary>
        /// Determines what the return type is for this method
        /// </summary>
        public Type ReturnType => InternalItem.ReturnType;

        /// <summary>
        /// Determines if method is abstract
        /// </summary>
        public bool IsAbstract => InternalItem.IsAbstract;

        /// <summary>
        /// Determines what the type is that has the base definition of this method
        /// </summary>
        public Type BaseDefinition => InternalItem.GetBaseDefinition()?.DeclaringType;

        /// <summary>
        /// Determines if method is overriden
        /// </summary>
        public bool IsOverriden => BaseDefinition != InternalItem.DeclaringType;

        /// <summary>
        /// Gets the parameters of this method
        /// </summary>
        public ParameterInfo[] Parameters => InternalItem.GetParameters();

        internal MarkdownMethod(MethodInfo info, bool isStatic)
        {
            InternalItem = info;
            IsStatic = isStatic;
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
