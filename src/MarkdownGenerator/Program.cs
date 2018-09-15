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
    class Program
    {
        [Value(0, Required = true, MetaName = "Dll Path")]
        public string DllPath { get; set; }

        [Value(1, Required = false, Default = "md", MetaName = "Output Directory")]
        public string Destination { get; set; }

        [Option("root-filename", HelpText="The name of the markdown file at the root of your documentation", Default = "Home")]
        public string RootFileName { get; set; }

        [Option("namespace-page", Default = false, HelpText = "Create pages for each namespace")]
        public bool NamespacePages { get; set; }

        [Option("method-page", Default = false, HelpText = "Create pages for each method")]
        public bool MethodPages { get; set; }

        [Usage(ApplicationAlias = "markdowngen")]
        public static IEnumerable<Example> Examples
        {
            get
            {
                yield return new Example("Normal Usage", new Program { DllPath = "./MyDll.dll", Destination = "./Api" });
            }
        }

        static void Main(string[] args) => Parser.Default.ParseArguments<Program>(args).MapResult(prog => Execute(prog), _ => 1);

        // 0 = dll src path, 1 = dest root
        static int Execute(Program file)
        {
            // put dll & xml on same diretory.
            string target = file.DllPath;
            string dest = file.Destination;
            string namespaceMatch = string.Empty;
            try
            {
                var types = MarkdownGenerator.Load(target, namespaceMatch);

                // Home Markdown Builder
                var homeBuilder = new MarkdownBuilder();
                homeBuilder.Header(1, "References");
                homeBuilder.AppendLine();

                foreach (var g in types.GroupBy(x => x.Namespace).OrderBy(x => x.Key))
                {
                    var namespaceDirectoryPath = Path.Combine(dest, g.Key.Replace('.', '\\'));
                    if (!Directory.Exists(namespaceDirectoryPath)) Directory.CreateDirectory(namespaceDirectoryPath);

                    var namespaceBuilder = new MarkdownBuilder();
                    namespaceBuilder.Header(1, g.Key);
                    namespaceBuilder.AppendLine();

                    if (!file.NamespacePages)
                        homeBuilder.HeaderWithLink(2, g.Key, g.Key);
                    else
                        homeBuilder.HeaderWithLink(2, g.Key, Path.Combine(g.Key.Replace('.', '\\'), file.RootFileName + ".md"));

                    homeBuilder.AppendLine();

                    foreach (var item in g.OrderBy(x => x.Name))
                    {
                        var sb = new StringBuilder();
                        homeBuilder.ListLink(MarkdownBuilder.MarkdownCodeQuote(item.BeautifyName), Path.Combine(g.Key.Replace('.', '\\'), item.Name + ".md"));
                        namespaceBuilder.ListLink(MarkdownBuilder.MarkdownCodeQuote(item.BeautifyName), Path.Combine(g.Key.Replace('.', '\\'), item.Name + ".md"));

                        sb.Append(item.ToString());
                        File.WriteAllText(Path.Combine(namespaceDirectoryPath, item.Name + ".md"), sb.ToString());
                        
                        if(!file.MethodPages)
                            item.GenerateMethodDocuments(namespaceDirectoryPath);
                    }

                    homeBuilder.AppendLine();
                    namespaceBuilder.AppendLine();
                    if (file.NamespacePages)
                        File.WriteAllText(Path.Combine(namespaceDirectoryPath, $"{file.RootFileName}.md"), namespaceBuilder.ToString());
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
