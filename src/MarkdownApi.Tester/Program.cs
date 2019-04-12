using System;
using igloo15.MarkdownApi.Core;
using igloo15.MarkdownApi.Core.Themes;
using igloo15.MarkdownApi.Core.Themes.Default;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace igloo15.MarkdownApi.Tester
{
    static class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            



            var factory = new LoggerFactory();

            factory.AddConsole();

            //var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");
            var project = MarkdownApiGenerator.GenerateProject("../../../MarkdownApi.Core/**/igloo15*.dll", "", factory);
            //var project = MarkdownApiGenerator.GenerateProject("../../../../../Nuget.Searcher/dist/**/publish/igloo15*.dll", factory);



            project.Build(new DefaultTheme(new DefaultOptions
                    {
                        BuildNamespacePages = true,
                        BuildTypePages = true,
                        RootFileName = "README.md",
                        RootTitle = "API",
                        RootSummary = "The Root Page Summary",
                        ShowParameterNames = true
                    }
                ),
                @"..\..\..\..\docs\api"
            );

            AppDomain.CurrentDomain.ProcessExit += (e, s) =>
            {
                factory.Dispose();
            };

            Console.ReadLine();
            
        }
    }
}
