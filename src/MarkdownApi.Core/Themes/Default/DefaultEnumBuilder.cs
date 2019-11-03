using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// The default enum page builder
    /// </summary>
    public class DefaultEnumBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// Constructs a enum page builder
        /// </summary>
        /// <param name="options">The default options</param>
        public DefaultEnumBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the markdown for the markdown page
        /// </summary>
        /// <param name="item">The markdown enum item</param>
        /// <returns>The markdown text</returns>
        public string BuildPage(MarkdownEnum item)
        {
            DefaultTheme.ThemeLogger?.LogDebug("Building Enum Page");
            var mb = new MarkdownBuilder();

            mb.HeaderWithCode(1, Cleaner.CreateFullTypeWithLinks(item, item.InternalType, false, false));

            item.BuildNamespaceLinks(item.Namespace, mb);

            if (_options.ShowAssembly)
                mb.Append("Assembly: ").AppendLine(item.InternalType.Module.Name).AppendLine();
            
            mb.AppendLine();

            var comments = item.Comments;

            if (!String.IsNullOrEmpty(item.Summary))
            {
                mb.AppendLine(item.Summary);
            }

            //mb.Append(GetCode(value));

            mb.AppendLine();

            BuildEnumTable(mb, comments, item);

            return mb.ToString();
        }


        private static void BuildEnumTable(MarkdownBuilder mb, IEnumerable<XmlDocumentComment> comments, MarkdownEnum value)
        {
            var underlyingEnumType = Enum.GetUnderlyingType(value.InternalType);
            var enums = value.EnumNames
                    .Select(x => new {
                        Name = x,
                        
                        Value = (Convert.ChangeType(Enum.Parse(value.InternalType, x), underlyingEnumType))
                    })
                    .OrderBy(x => x.Value)
                    .ToArray();

            if (enums.Any())
            {
                mb.AppendLine($"##\tEnum");
                mb.AppendLine();

                string[] head = new[] { "Value", "Name", "Summary" };

                var data = enums.Select(item =>
                {
                    var summary = comments.FirstOrDefault(x => x.MemberName == item.Name
                    || x.MemberName.StartsWith(item.Name + "`"))?.Summary ?? "";


                    return new[] {
                        item.Value.ToString(),
                        item.Name,
                        summary
                    };
                });

                mb.Table(head, data, true);
                mb.AppendLine();
            }
        }
    }
}
