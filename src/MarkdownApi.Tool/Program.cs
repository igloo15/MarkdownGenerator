using CommandLine;
using CommandLine.Text;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.Themes;
using Microsoft.Extensions.Logging;

namespace Igloo15.MarkdownApi.Tool
{
        
    static class Program
    {

        static void Main(string[] args) => Parser.Default.ParseArguments<Options>(args).MapResult(prog => Execute(prog), _ => 1);

        // 0 = dll src path, 1 = dest root
        static int Execute(Options file)
        {
            // put dll & xml on same directory.
            string target = file.DllPath;
            string dest = file.Destination;
            string namespaceMatch = string.Empty;

            var factory = new LoggerFactory();

            factory.AddConsole();

            ILogger logger = factory.CreateLogger("MarkdownApi");

            AppDomain.CurrentDomain.ProcessExit += (e, s) =>
            {
                factory.Dispose();
            };

            try
            {

                logger.LogInformation("Beginning operation with {searchArea} search area and {namespaceMatch} filter \n outputing to {destination}", target, namespaceMatch, dest);
                var project = MarkdownApiGenerator.GenerateProject(target, namespaceMatch, factory);


                project.Build(new DefaultTheme(file.GenerateOptions()), dest);

                
            }
            catch (Exception e)
            {
                logger.LogError(e, "Failed to Create Documentation");
                factory.Dispose();
                var result = Parser.Default.ParseArguments<Options>(new string[] { });

                return 1;
            }

            return 0;
        }

        
    }
}
