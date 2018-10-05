using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public class MarkdownEnum : AbstractType
    {
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Enum;
        
        public List<string> EnumNames => InternalType.GetEnumNames().ToList();

        public override string BuildPage(ITheme theme) => theme.BuildPage(this);

        internal MarkdownEnum() { }
    }
}