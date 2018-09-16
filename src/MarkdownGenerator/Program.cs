using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Igloo15.MarkdownGenerator
{
    internal class Options
    {
        [Value(0, Required = true, MetaName = "Dll Path", HelpText = "The path to the dll to create documentation for. May include wildcards on file name. Use ';' to search multiple areas")]
        public string DllPath { get; set; }

        [Value(1, Required = false, Default = "md", MetaName = "Output Directory", HelpText = "The root folder to put documentation in")]
        public string Destination { get; set; }

        [Option("root-filename", HelpText = "The name of the markdown file at the root of your documentation", Default = "Home")]
        public string RootFileName { get; set; }

        [Option("title", Default = "Api", HelpText = "Title of the root home page")]
        public string RootTitle { get; set; }

        [Option("clean-destination", Default = false, HelpText = "Deletes all content in destination before generating new content")]
        public bool CleanDestination { get; set; }

        [Option("namespace-page", Default = false, HelpText = "Create pages for each namespace")]
        public bool NamespacePages { get; set; }

        [Option("type-page", Default = true, HelpText = "Create pages for each type")]
        public bool TypePages { get; set; }

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

        [Option("property-folder", Default = "Properties", HelpText = "The folder to store property pages in")]
        public string PropertyFolderName { get; set; }

        [Option("field-folder", Default = "Fields", HelpText = "The folder to store field pages in")]
        public string FieldFolderName { get; set; }

        [Option("event-folder", Default = "Events", HelpText = "The folder to store event pages in")]
        public string EventFolderName { get; set; }

        [Usage(ApplicationAlias = "markdowngen")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Normal Usage", new Options { DllPath = "./MyDll.dll", Destination = "./Api" });
            }
        }
    }

    class Program
    {

        static void Main(string[] args) => Parser.Default.ParseArguments<Options>(args).MapResult(prog => Execute(prog), _ => 1);

        // 0 = dll src path, 1 = dest root
        static int Execute(Options file)
        {
            // put dll & xml on same directory.
            string target = file.DllPath;
            string dest = file.Destination;
            string namespaceMatch = string.Empty;
            try
            {
                

                var namespaceGroups = MarkdownGenerator.Load(target, namespaceMatch, file);

                
            }
            catch (Exception e)
            {
                return 1;
            }
            

            return 0;
        }
    }
}
