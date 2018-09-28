using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    public static class MarkdownApiGenerator
    {
        public static MarkdownProject GenerateProject(string searchArea, string namespaceMatch)
        {
            var project = MarkdownItemBuilder.Load(searchArea, namespaceMatch);

            project.AllItems = MarkdownRepo.GetLookup();

            return project;
        }
    }
}
