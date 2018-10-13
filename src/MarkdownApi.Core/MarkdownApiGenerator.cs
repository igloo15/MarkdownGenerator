using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    /// <summary>
    /// 
    /// </summary>
    public static class MarkdownApiGenerator
    {
        
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchArea"></param>
        /// <param name="namespaceMatch"></param>
        /// <param name="factory"></param>
        /// <returns></returns>
        public static MarkdownProject GenerateProject(string searchArea, string namespaceMatch, ILoggerFactory factory = null)
        {
            Constants.Logger = factory?.CreateLogger("Generation");

            Constants.Logger?.LogInformation("Beginning Loading of dlls and xml files using search area {searchArea}", searchArea);
            var project = MarkdownItemBuilder.Load(searchArea, namespaceMatch);

            Constants.Logger?.LogInformation("Generating Lookup of all Markdown Items found");
            project.AllItems = MarkdownRepo.GetLookup();

            Constants.Logger?.LogInformation("{markdownItemCount} Markdown Items found with {searchArea}", project.AllItems.Count, searchArea);

            return project;
        }
    }
}
