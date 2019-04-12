using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Microsoft.Extensions.FileSystemGlobbing;

namespace Igloo15.MarkdownApi.Core
{
    /// <summary>
    /// The main class used to generate a project based on the search area
    /// </summary>
    public static class MarkdownApiGenerator
    {

        /// <summary>
        /// Generate an Markdown Api Project with the given search area, namespacematch and logger factory
        /// </summary>
        /// <param name="searchArea">This should be a relative path from the current directory using forward slashes. A globber pattern may be used. Also it must end in .dll. Additionally you may include multiple patterns separating each with ';'</param>
        /// <param name="namespaceMatch">If provided will only build items that start with the provide string</param>
        /// <param name="factory">The logger factory</param>
        /// <returns>The markdown project containing the items to be rendered</returns>
        public static MarkdownProject GenerateProject(string searchArea, string namespaceMatch = "", ILoggerFactory factory = null)
        {
            Constants.Logger = factory?.CreateLogger("MarkdownApiGenerator");

            if(searchArea.Contains("\\"))
            {
                Constants.Logger?.LogError("Search Area must only contain forward slashes /");
                return new MarkdownProject();
            }

            Constants.Logger?.LogInformation("Beginning Loading of dlls and xml files using search area {searchArea}", searchArea);
            var project = MarkdownItemBuilder.Load(searchArea, namespaceMatch);

            Constants.Logger?.LogInformation("Generating Lookup of all Markdown Items found");
            project.AllItems = MarkdownRepo.GetLookup();

            Constants.Logger?.LogInformation("{markdownItemCount} Markdown Items found with {searchArea}", project.AllItems.Count, searchArea);

            return project;
        }

        /// <summary>
        /// Generate an Markdown Api Project with the given search area and logger factory
        /// </summary>
        /// <param name="searchArea">This should be a relative path from the current directory using forward slashes. A globber pattern may be used. Also it must end in .dll. Additionally you may include multiple patterns separating each with ';'</param>
        /// <param name="factory">The logger factory</param>
        /// <returns>The markdown project containing the items to be rendered</returns>
        public static MarkdownProject GenerateProject(string searchArea, ILoggerFactory factory)
        {
            return GenerateProject(searchArea, "", factory);
        }
    }
}
