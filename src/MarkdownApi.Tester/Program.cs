using System;
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.Themes;
using Igloo15.MarkdownApi.Core.Themes.Default;

namespace Igloo15.MarkdownApi.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            //var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");
            var project = MarkdownApiGenerator.GenerateProject(@"..\..\..\MarkdownApi.Core\Debug\netstandard2.0\*.dll", "", "Api");



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


            

            Console.WriteLine(project);

            //Console.ReadLine();

        }
    }
}
