using Igloo15.MarkdownGenerator.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultProjectPart : IThemePart<MarkdownableProject>
    {
        public DefaultProjectPart(ITheme rootTheme)
        {
            RootTheme = rootTheme;
        }

        public ITheme RootTheme { get; private set; }

        public string GetCode(MarkdownableProject value)
        {
            return "";
        }

        public string GetDetailed(MarkdownableProject value)
        {
            return "";
        }

        public string GetExample(MarkdownableProject value)
        {
            return "";
        }

        public string GetLink(MarkdownableProject value)
        {
            return $"[{value.Name}]({Path.Combine(value.FolderPath, $"{value.Config.RootFileName}.md")}";
        }

        public string GetName(MarkdownableProject value)
        {
            return value.Name;
        }

        public string GetReturnOrType(MarkdownableProject value)
        {
            return "";
        }

        public string GetSummary(MarkdownableProject value)
        {
            return value.Config.Summary;
        }

        public string[] GetTableHeaders()
        {
            return new string[] { "" };
        }

        public string GetPage(MarkdownableProject value)
        {
            var homeBuilder = new MarkdownBuilder();
            homeBuilder.Header(1, value.Config.RootTitle);
            homeBuilder.AppendLine();

            foreach (var g in value.Namespaces)
            {
                if (value.Config.NamespacePages)
                {
                    homeBuilder.Header(2, RootTheme.NamespacePart.GetLink(g));
                }
                else
                {
                    homeBuilder.Header(2, RootTheme.NamespacePart.GetName(g));
                }

                homeBuilder.AppendLine();

                foreach (var item in g.Types.OrderBy(x => x.Name))
                {
                    var sb = new StringBuilder();


                    if (value.Config.TypePages)
                    {
                        homeBuilder.List(RootTheme.TypePart.GetLink(item));
                    }
                    else
                    {
                        homeBuilder.List(RootTheme.TypePart.GetName(item));
                    }
                }
            }

            homeBuilder.AppendLine();

            return homeBuilder.ToString();
        }
    }
}