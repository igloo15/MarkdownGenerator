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


            var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");

           

            project.Build(new DefaultTheme(new DefaultOptions
                    {
                        BuildNamespacePages = true,
                        BuildTypePages = true,
                        RootFolderName = "./md",
                        RootFileName = "Home.md"
                    }
                )
            );


            

            Console.WriteLine(project);

            //Console.ReadLine();

        }
    }
}
