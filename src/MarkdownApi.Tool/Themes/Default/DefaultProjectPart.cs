using Igloo15.MarkdownGenerator.Models;
using System.IO;
using System.Linq;
using System.Reflection;
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

        public string GetLink(MarkdownableProject value, MemberInfo from)
        {
            var mb = new MarkdownBuilder();
            var toPath = $"{value.FolderPath}";
            var folder = Extensions.RelativePath(from.GetMemberInfoFolder(), toPath);

            if (from == null)
                mb.Link(GetName(value), value.FilePath);
            else
                mb.Link(GetName(value), Path.Combine(folder, $"{value.Config.RootFileName}.md"));

            return mb.ToString();
        }

        public string GetName(MarkdownableProject value)
        {
            return value.Name;
        }

        public MemberInfo GetReturnOrType(MarkdownableProject value)
        {
            return null;
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
                    homeBuilder.Header(2, g.Config.CurrentTheme.NamespacePart.GetLink(g, g.Types.FirstOrDefault().InternalType));
                }
                else
                {
                    homeBuilder.Header(2, g.GetName());
                }

                homeBuilder.AppendLine();

                foreach (var item in g.Types.OrderBy(x => x.Name))
                {
                    var sb = new StringBuilder();


                    if (value.Config.TypePages)
                    {
                        homeBuilder.List(item.Config.CurrentTheme.TypePart.GetLink(item, null));
                    }
                    else
                    {
                        homeBuilder.List(item.GetName());
                    }
                }
            }

            homeBuilder.AppendLine();

            return homeBuilder.ToString();
        }
    }
}