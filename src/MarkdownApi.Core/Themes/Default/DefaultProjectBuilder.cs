using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// A markdown project page builder this is the root of all the markdown api content
    /// </summary>
    public class DefaultProjectBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// Constructs a markdown project page builder
        /// </summary>
        /// <param name="options">The default options for page building</param>
        public DefaultProjectBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the content for the markdown project page
        /// </summary>
        /// <param name="project">The markdown project item to be rendered</param>
        /// <returns>The markdown content</returns>
        public string BuildPage(MarkdownProject project)
        {
            DefaultTheme.ThemeLogger?.LogDebug("Building Main Api Page");
            var homeBuilder = new MarkdownBuilder();
            homeBuilder.HeaderWithLink(1, _options.RootTitle, project.To(project));
            homeBuilder.AppendLine();

            if (!string.IsNullOrEmpty(_options.RootSummary))
            {
                homeBuilder
                    .Header(2, "Summary")
                    .AppendLine(_options.RootSummary)
                    .AppendLine();
            }

            homeBuilder.Header(2, "Namespaces").AppendLine();

            foreach (var tempItem in project.AllItems.Values.Where(i => i.ItemType == MarkdownItemTypes.Namespace))
            {
                var g = tempItem.As<MarkdownNamespace>();

                if (!String.IsNullOrEmpty(g.FileName))
                {
                    homeBuilder.HeaderWithLink(3, g.FullName, project.To(g));
                }
                else
                {
                    homeBuilder.Header(3, g.Name);
                }

                homeBuilder.AppendLine();

                if (!_options.ShowTypesOnRootPage)
                    continue;

                foreach (var item in g.Types.OrderBy(x => x.Name))
                {
                    if (!String.IsNullOrEmpty(item.FileName))
                    {
                        homeBuilder.List(Cleaner.CreateFullTypeWithLinks(project, item.InternalType, false, true));
                    }
                    else
                    {
                        homeBuilder.List(item.Name);
                    }
                }
            }

            homeBuilder.AppendLine();

            return homeBuilder.ToString();
        }
    }
}
