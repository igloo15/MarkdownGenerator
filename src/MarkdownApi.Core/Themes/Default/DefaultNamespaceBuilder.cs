using Igloo15.MarkdownApi.Core.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    internal static class DefaultNamespaceBuilder
    {
        internal static string BuildPage(MarkdownNamespace item)
        {

            var namespaceBuilder = new MarkdownBuilder();
            namespaceBuilder.HeaderWithLink(1, item.FullName, item.To(item));
            namespaceBuilder.AppendLine();

            foreach (var type in item.Types.OrderBy(x => x.Name))
            {
                var sb = new StringBuilder();
                if (!String.IsNullOrEmpty(type.FileName))
                {
                    namespaceBuilder.List(Cleaner.CreateFullTypeWithLinks(item, type.InternalType, false));
                }
                else
                {
                    namespaceBuilder.List(item.Name);
                }
            }

            namespaceBuilder.AppendLine();


            return namespaceBuilder.ToString();
        }
    }
}
