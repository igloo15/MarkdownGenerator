using CommandLine;
using CommandLine.Text;
using Igloo15.MarkdownApi.Core.Themes.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Tool
{
    internal class Options
    {
        [Value(0, Required = true, MetaName = "Dll Path", HelpText = "The path to the dll to create documentation for. May include wildcards on file name. Use ';' to search multiple areas")]
        public string DllPath { get; set; }

        [Value(1, Required = false, Default = "md", MetaName = "Output Directory", HelpText = "The root folder to put documentation in")]
        public string Destination { get; set; }

        [Option("namespace-filter", HelpText = "A regex used to generate documentation only for namespaces that match", Default = "")]
        public string NamespaceFilter { get; set; }

        [Option("root-filename", HelpText = "The name of the markdown file at the root of your documentation", Default = "README.md")]
        public string RootFileName { get; set; }

        [Option("title", Default = "Api", HelpText = "Title of the root home page")]
        public string RootTitle { get; set; }

        [Option("summary", Default = "", HelpText = "A summary you want to appear on root page")]
        public string Summary { get; set; }
        
        [Option("namespace-page", Default = false, HelpText = "Create pages for each namespace")]
        public bool NamespacePages { get; set; }

        [Option("type-page", Default = true, HelpText = "Create pages for each type")]
        public bool TypePages { get; set; }

        [Option("constructor-page", Default = false, HelpText = "Create pages for each constructor")]
        public bool ConstructorPages { get; set; }

        [Option("method-page", Default = false, HelpText = "Create pages for each method")]
        public bool MethodPages { get; set; }

        [Option("property-page", Default = false, HelpText = "Create pages for each property")]
        public bool PropertyPages { get; set; }

        [Option("field-page", Default = false, HelpText = "Create pages for each field")]
        public bool FieldPages { get; set; }

        [Option("event-page", Default = false, HelpText = "Create pages for each event")]
        public bool EventPages { get; set; }

        [Option("method-folder", Default = "Methods", HelpText = "The folder to store method pages in")]
        public string MethodFolderName { get; set; }

        [Option("constructors-folder", Default = "Constructors", HelpText = "The folder to store constructor pages in")]
        public string ConstructorsFolderName { get; set; }

        [Option("property-folder", Default = "Properties", HelpText = "The folder to store property pages in")]
        public string PropertyFolderName { get; set; }

        [Option("field-folder", Default = "Fields", HelpText = "The folder to store field pages in")]
        public string FieldFolderName { get; set; }

        [Option("event-folder", Default = "Events", HelpText = "The folder to store event pages in")]
        public string EventFolderName { get; set; }

        [Option("theme", Default = "Default", HelpText = "The theme you wish to use. Selecting a theme will potentially override the commandline arguments you have defined")]
        public string ThemeName { get; set; }
        
        [Usage(ApplicationAlias = "markdownapi")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Normal Usage", new Options { DllPath = "./MyDll.dll", Destination = "./Api" });
                yield return new Example("Wildcard Usage", new Options { DllPath = "./bin/*.dll", Destination = "./Api" });
                yield return new Example("Multiple Search Locations", new Options { DllPath = "./bin/*.dll;./dist/myapp/myApp.dll", Destination = "../../..docs/Api" });
            }
        }

        public DefaultOptions GenerateOptions()
        {
            DefaultOptions options = new DefaultOptions();


            options.RootTitle = RootTitle;
            options.RootFileName = RootFileName;
            options.RootSummary = Summary;
            options.BuildTypePages = TypePages;
            options.BuildNamespacePages = NamespacePages;
            options.BuildConstructorPages = ConstructorPages;
            options.BuildMethodPages = MethodPages;
            options.MethodFolderName = MethodFolderName;
            options.ConstructorFolderName = ConstructorsFolderName;

            return options;
        }
    }
}
