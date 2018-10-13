using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultNamespaceBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DefaultNamespaceBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string BuildPage(MarkdownNamespace item)
        {
            DefaultTheme.ThemeLogger?.LogDebug("Building Namespace Page");
            var namespaceBuilder = new MarkdownBuilder();
            namespaceBuilder.HeaderWithLink(1, item.FullName, item.To(item));
            namespaceBuilder.AppendLine();

            foreach (var type in item.Types.OrderBy(x => x.Name))
            {
                var sb = new StringBuilder();
                if (!String.IsNullOrEmpty(type.FileName))
                {
                    namespaceBuilder.List(Cleaner.CreateFullTypeWithLinks(item, type.InternalType, false, true));
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
