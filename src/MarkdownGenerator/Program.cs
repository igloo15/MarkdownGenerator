using CommandLine;
using CommandLine.Text;
using Igloo15.MarkdownGenerator.Themes.Default;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Igloo15.MarkdownGenerator
{
        
    class Program
    {

        static void Main(string[] args) => Parser.Default.ParseArguments<Options>(args).MapResult(prog => Execute(prog), _ => 1);

        // 0 = dll src path, 1 = dest root
        static int Execute(Options file)
        {
            InitializeThemes();
            // put dll & xml on same directory.
            string target = file.DllPath;
            string dest = file.Destination;
            string namespaceMatch = string.Empty;
            
            try
            {
                

                var project = MarkdownGenerator.Load(target, namespaceMatch, file);


                project.Build(dest, file);

                
            }
            catch (Exception e)
            {
                Console.Error.WriteLine(e);

                var result = Parser.Default.ParseArguments<Options>(new string[] { });

                return 1;
            }
            

            return 0;
        }

        internal static Dictionary<string, ITheme> Themes { get; private set; }

        private static void InitializeThemes()
        {
            var collection = new ServiceCollection();

            collection.Scan(scan => scan.FromAssemblyOf<Program>().AddClasses(classes => classes.AssignableTo<ITheme>()).As<ITheme>().WithSingletonLifetime());

            var provider = collection.BuildServiceProvider();

            Themes = provider.GetServices<ITheme>().ToDictionary(t => t.Name.ToLower());
        }

        internal static ITheme SearchThemes(string themeName)
        {
            var theme = Themes[themeName.ToLower()];

            if (theme == null)
                throw new KeyNotFoundException($"Theme : {themeName} not found");

            return theme;
        }
    }
}
