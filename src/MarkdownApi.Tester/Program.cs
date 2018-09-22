using System;
using Igloo15.MarkdownApi.Core;
using Igloo15.MarkdownApi.Core.Themes;
using Igloo15.MarkdownApi.Core.TypeParts;

namespace Igloo15.MarkdownApi.Tester
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");


            var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");



            project.Build(new DefaultTheme("Home.md", "./md"));


            

            Console.WriteLine(project);

            Console.ReadLine();

        }
    }
}
