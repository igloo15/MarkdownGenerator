using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultOptions
    {
        public bool BuildNamespacePages { get; set; } = true;

        public bool BuildTypePages { get; set; } = true;

        public bool BuildMethodPages { get; set; } = false;

        public bool BuildConstructorPages { get; set; } = false;

        public bool ShowTypesOnRootPage { get; set; } = true;

        public string MethodFolderName { get; set; } = "Methods";

        public string ConstructorFolderName { get; set; } = "Constructors";

        public string RootFileName { get; set; } = "Home.md";

        public string RootTitle { get; set; } = "API";

        public string RootSummary { get; set; }

        public bool ShowAssembly { get; set; } = true;

        public bool ShowParameterNames { get; set; } = false;
    }
}
