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
        [Value(0, Required = true, MetaName = "Dll Path")]
        public string DllPath { get; set; }

        [Value(1, Required = false, Default = "md", MetaName = "Output Directory")]
        public string Destination { get; set; }

        [Option("root-filename", HelpText = "The name of the markdown file at the root of your documentation", Default = "Home")]
        public string RootFileName { get; set; }

        [Option("namespace-page", Default = false, HelpText = "Create pages for each namespace")]
        public bool NamespacePages { get; set; }

        [Option("method-page", Default = false, HelpText = "Create pages for each method")]
        public bool MethodPages { get; set; }

        [Option("clean-destination", Default = false, HelpText = "Deletes all content in destination before generating new content")]
        public bool CleanDestination { get; set; }

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
            // put dll & xml on same diretory.
            string target = file.DllPath;
            string dest = file.Destination;
            string namespaceMatch = string.Empty;
            try
            {
                var destination = new DirectoryInfo(dest);
                if (!destination.Exists)
                    Directory.CreateDirectory(dest);

                if (file.CleanDestination)
                    destination.Empty();

                var namespaceGroups = MarkdownGenerator.Load(target, namespaceMatch);

                // Home Markdown Builder
                var homeBuilder = new MarkdownBuilder();
                homeBuilder.Header(1, "References");
                homeBuilder.AppendLine();

                foreach (var g in namespaceGroups)
                {
                    g.Build(dest, file, homeBuilder);
                }

                // Gen Home
                File.WriteAllText(Path.Combine(dest, $"{file.RootFileName}.md"), homeBuilder.ToString());
            }
            catch (Exception e)
            {
                return 1;
            }
            

            return 0;
        }
    }
}
