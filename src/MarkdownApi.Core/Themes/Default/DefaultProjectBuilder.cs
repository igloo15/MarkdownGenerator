using Igloo15.MarkdownApi.Core.Builders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    internal static class DefaultProjectBuilder
    {
        public static string BuildPage(MarkdownProject project)
        {
            var homeBuilder = new MarkdownBuilder();
            homeBuilder.Header(1, project.Name);
            homeBuilder.AppendLine();

            foreach (var g in project.Namespaces)
            {
                if (!String.IsNullOrEmpty(g.FileName))
                {
                    homeBuilder.HeaderWithLink(2, g.Name, g.Location.CombinePath(g.FileName).AddRoot());
                }
                else
                {
                    homeBuilder.Header(2, g.Name);
                }

                homeBuilder.AppendLine();

                foreach (var item in g.Types.OrderBy(x => x.Name))
                {
                    if (!String.IsNullOrEmpty(item.FileName))
                    {
                        homeBuilder.ListLink(item.Name, project.To(item));
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
