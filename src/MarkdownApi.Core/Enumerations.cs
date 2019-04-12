using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{

    /// <summary>
    /// An enumeration of the markdownitem types
    /// </summary>
    public enum MarkdownItemTypes
    {
        /// <summary>
        /// A namespace type
        /// </summary>
        Namespace,
        /// <summary>
        /// A basic type type
        /// </summary>
        Type,
        /// <summary>
        /// An enum type
        /// </summary>
        Enum,
        /// <summary>
        /// A constructor type
        /// </summary>
        Constructor,
        /// <summary>
        /// A method type
        /// </summary>
        Method,
        /// <summary>
        /// A property type
        /// </summary>
        Property,
        /// <summary>
        /// A field type
        /// </summary>
        Field,
        /// <summary>
        /// A event type
        /// </summary>
        Event,
        /// <summary>
        /// A parameter type
        /// </summary>
        Parameter,
        /// <summary>
        /// A return type
        /// </summary>
        Return, 
        /// <summary>
        /// A project type
        /// </summary>
        Project
    }

}
