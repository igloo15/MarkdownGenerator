﻿using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultEnumBuilder
    {
        private DefaultOptions _options;

        public DefaultEnumBuilder(DefaultOptions options)
        {
            _options = options;
        }

        public string BuildPage(MarkdownEnum item)
        {
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

            var enums = value.EnumNames
                    .Select(x => new {
                        Name = x,
                        //Value = ((Int32)Enum.Parse(type),
                        Value = x
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
                        item.Value,
                        item.Name,
                        summary
                    };
                });

                mb.Table(head, data);
                mb.AppendLine();
            }
        }
    }
}