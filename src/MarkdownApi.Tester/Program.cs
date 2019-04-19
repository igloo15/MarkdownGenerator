using System;
using System.Text.RegularExpressions;
using igloo15.MarkdownApi.Core;
using igloo15.MarkdownApi.Core.Themes;
using igloo15.MarkdownApi.Core.Themes.Default;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Console;

namespace igloo15.MarkdownApi.Tester
{
    internal static class Program
    {
        private static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            var test = "igloo15.MarkdownApi.Tests.MarkdownTestGenericClass{igloo15.MarkdownApi.Tests.MarkdownTestGenericClass{System.String[0:,0:],System.Collections.Generic.List{System.String[0:,0:]},System.String},System.String,System.String[0:,0:]},igloo15.MarkdownApi.Tests.MarkdownTestGenericClass{``0,System.String,System.String[0:,0:]}";
            var shiz = new System.Collections.Generic.List<string>();
            var nestCount = 0;
            var startIdx = 0;
            for (int i = 0; i < test.Length; i++)
            {
                if (test[i] == ',' && nestCount == 0)
                {
                    shiz.Add(test.Substring(startIdx, i - startIdx));
                    startIdx = i + 1;
                }
                else if (i == test.Length - 1)
                {
                    shiz.Add(test.Substring(startIdx, test.Length - startIdx));
                }
                else if (test[i] == '[' || test[i] == '{')
                {
                    nestCount++;
                }
                else if (test[i] == ']' || test[i] == '}')
                {
                    nestCount--;
                }
            }

            var factory = new LoggerFactory();

            factory.AddConsole();

            //var project = MarkdownApiGenerator.GenerateProject(@"D:\Development\Projects\Nuget.Searcher\dist\NuGetSearcher\Release\netstandard2.0\publish\*.dll", "", "Api");
            var project = MarkdownApiGenerator.GenerateProject("../../../**/igloo15.MarkdownApi.Tests.dll", "", factory);
            //var project = MarkdownApiGenerator.GenerateProject("../../../../../Nuget.Searcher/dist/NuGetSearcher/Release/netstandard2.0/publish/igloo15.NuGetSearcher.dll", factory);

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