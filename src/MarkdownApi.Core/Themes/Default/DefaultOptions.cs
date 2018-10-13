using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultOptions
    {
        /// <summary>
        /// 
        /// </summary>
        public bool BuildNamespacePages { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool BuildTypePages { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool BuildMethodPages { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool BuildConstructorPages { get; set; } = false;

        /// <summary>
        /// 
        /// </summary>
        public bool ShowTypesOnRootPage { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public string MethodFolderName { get; set; } = "Methods";

        /// <summary>
        /// 
        /// </summary>
        public string ConstructorFolderName { get; set; } = "Constructors";

        /// <summary>
        /// 
        /// </summary>
        public string RootFileName { get; set; } = "Home.md";

        /// <summary>
        /// 
        /// </summary>
        public string RootTitle { get; set; } = "API";

        /// <summary>
        /// 
        /// </summary>
        public string RootSummary { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public bool ShowAssembly { get; set; } = true;

        /// <summary>
        /// 
        /// </summary>
        public bool ShowParameterNames { get; set; } = false;
    }
}
