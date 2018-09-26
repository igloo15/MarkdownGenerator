using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultOptions
    {
        public bool BuildNamespacePages { get; set; }

        public bool BuildTypePages { get; set; }

        public bool BuildMethodPages { get; set; }

        public bool BuildConstructorPages { get; set; }

        public bool ShowTypesOnRootPage { get; set; }

        public string MethodFolderName { get; set; }

        public string ConstructorFolderName { get; set; }

        public string RootFolderName { get; set; }

        public string RootFileName { get; set; }

        public string RootTitle { get; set; }

        public string RootSummary { get; set; }
    }
}
