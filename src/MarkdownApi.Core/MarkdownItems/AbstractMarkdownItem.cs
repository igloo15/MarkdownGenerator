using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public abstract class AbstractMarkdownItem : IMarkdownItem
    {
        public abstract MarkdownProject Project { get; }

        public abstract MarkdownItemTypes ItemType { get; }

        public abstract TypeWrapper TypeInfo { get; }

        public string Location { get; internal set; }

        public string FileName { get; internal set; }

        public abstract string Name { get; }

        public abstract string FullName { get; }

        public string Summary { get; internal set; }

        public abstract string BuildPage(ITheme theme);

        
    }
}
