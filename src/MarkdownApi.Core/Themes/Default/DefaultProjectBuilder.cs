using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using System;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultProjectBuilder
    {
        private DefaultOptions _options;

        public DefaultProjectBuilder(DefaultOptions options)
        {
            _options = options;
        }

        public string BuildPage(MarkdownProject project)
        {
            var homeBuilder = new MarkdownBuilder();
            homeBuilder.HeaderWithLink(1, project.Name, project.To(project));
            homeBuilder.AppendLine();

            foreach (var g in project.Namespaces)
            {
                if (!String.IsNullOrEmpty(g.FileName))
                {
                    homeBuilder.HeaderWithLink(2, g.FullName, project.To(g));
                }
                else
                {
                    homeBuilder.Header(2, g.Name);
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
