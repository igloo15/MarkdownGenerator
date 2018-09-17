using Igloo15.MarkdownGenerator.Models;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultNamespacePart : IThemePart<MarkdownableNamespace>
    {
        private DefaultTheme defaultTheme;

        public DefaultNamespacePart(DefaultTheme defaultTheme)
        {
            this.defaultTheme = defaultTheme;
        }

        public ITheme RootTheme => defaultTheme;

        public string GetCode(MarkdownableNamespace value)
        {
            return $"namespace {value.FullName} {{  }}";
        }

        public string GetDetailed(MarkdownableNamespace value)
        {
            return string.Empty;
        }

        public string GetExample(MarkdownableNamespace value)
        {
            return string.Empty;
        }

        public string GetLink(MarkdownableNamespace value)
        {
            return $"[{value.FullName}]({value.FolderPath}{Path.DirectorySeparatorChar}{value.Config.RootFileName}.md)";
        }

        public string GetName(MarkdownableNamespace value)
        {
            return value.FullName;
        }

        public string GetReturnOrType(MarkdownableNamespace value)
        {
            return "";
        }

        public string GetSummary(MarkdownableNamespace value)
        {
            return "";
        }

        public string[] GetTableHeaders()
        {
            return new[] { string.Empty };
        }

        public string GetPage(MarkdownableNamespace value)
        {
            var namespaceBuilder = new MarkdownBuilder();
            namespaceBuilder.Header(1, GetName(value));
            namespaceBuilder.AppendLine();

            foreach (var item in value.Types.OrderBy(x => x.Name))
            {
                var sb = new StringBuilder();
                if (value.Config.TypePages)
                {
                    namespaceBuilder.List(item.GetLink());
                }
                else
                {
                    namespaceBuilder.List(item.GetName());
                }
            }

            namespaceBuilder.AppendLine();


            return namespaceBuilder.ToString();
        }
    }
}