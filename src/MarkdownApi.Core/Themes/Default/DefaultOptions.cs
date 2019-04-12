using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// The default options that can be set on the default theme
    /// </summary>
    public class DefaultOptions
    {
        /// <summary>
        /// Build namespace pages defaults to true
        /// </summary>
        public bool BuildNamespacePages { get; set; } = true;

        /// <summary>
        /// Build Type pages defaults to true
        /// </summary>
        public bool BuildTypePages { get; set; } = true;

        /// <summary>
        /// Build Method Pages defaults to false
        /// </summary>
        public bool BuildMethodPages { get; set; } = false;

        /// <summary>
        /// Build Constructor Pages defaults to false
        /// </summary>
        public bool BuildConstructorPages { get; set; } = false;

        /// <summary>
        /// Show the types on the root page defaults to true
        /// </summary>
        public bool ShowTypesOnRootPage { get; set; } = true;

        /// <summary>
        /// The name of the folder containing method pages
        /// </summary>
        public string MethodFolderName { get; set; } = "Methods";

        /// <summary>
        /// The name of the folder containing constructor pages
        /// </summary>
        public string ConstructorFolderName { get; set; } = "Constructors";

        /// <summary>
        /// The name of the root file name of the api documentation
        /// </summary>
        public string RootFileName { get; set; } = "Home.md";

        /// <summary>
        /// The title in the root file
        /// </summary>
        public string RootTitle { get; set; } = "API";

        /// <summary>
        /// The summary text provided in the root file 
        /// </summary>
        public string RootSummary { get; set; }

        /// <summary>
        /// Determines if the assembly from which the markdown page came from is shown defaults to true
        /// </summary>
        public bool ShowAssembly { get; set; } = true;

        /// <summary>
        /// Determines if the parameter names should be shown defaults to false
        /// </summary>
        public bool ShowParameterNames { get; set; } = false;
    }
}
