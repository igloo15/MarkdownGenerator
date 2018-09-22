using System;
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.Themes;

namespace Igloo15.MarkdownApi.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "md");

            project.Resolve(new DefaultTheme("Home.md"));

            Console.WriteLine(project);

            Console.ReadLine();

        }
    }
}
