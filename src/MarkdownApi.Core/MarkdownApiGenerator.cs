using Igloo15.MarkdownApi.Core.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    public static class MarkdownApiGenerator
    {
        public static MarkdownProject GenerateProject(string searchArea, string namespaceMatch, string name)
        {
            var project = MarkdownItemBuilder.Load(searchArea, namespaceMatch, name);

            project.AllItems = MarkdownRepo.GetLookup();

            return project;
        }
    }
}
