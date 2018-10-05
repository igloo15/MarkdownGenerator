using System;
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.Themes;
using Igloo15.MarkdownApi.Core.Themes.Default;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace Igloo15.MarkdownApi.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            



            var factory = new LoggerFactory();

            factory.AddConsole();

            //var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");
            var project = MarkdownApiGenerator.GenerateProject(@"..\..\..\MarkdownApi.Core\Debug\netstandard2.0\*.dll", "", factory);



            project.Build(new DefaultTheme(new DefaultOptions
                    {
                        BuildNamespacePages = true,
                        BuildTypePages = true,
                        RootFileName = "README.md",
                        RootTitle = "API",
                        ShowParameterNames = true
                    }
                ),
                @"..\..\..\..\docs\api"
            );

            AppDomain.CurrentDomain.ProcessExit += (e, s) =>
            {
                factory.Dispose();
            };
            
            
        }
    }
}
