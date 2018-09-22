using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using Igloo15.MarkdownApi.Core.Themes.Default;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes
{
    public class DefaultTheme : ITheme
    {
        public string RootName { get; set; }

        public DefaultTheme(string rootName)
        {
            RootName = rootName;
        }

        public IResolver Resolver => new DefaultResolver(RootName);

        public string BuildPage(MarkdownNamespace item)
        {
            return DefaultNamespaceBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownProject item)
        {
            return DefaultProjectBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownType item)
        {
            return DefaultTypeBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownEnum item)
        {
            return DefaultEnumBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownField item)
        {
            return "";
        }

        public string BuildPage(MarkdownProperty item)
        {
            return "";
        }

        public string BuildPage(MarkdownMethod item)
        {
            return "";
        }

        public string BuildPage(MarkdownEvent item)
        {
            return "";
        }
    }
}
