using Igloo15.MarkdownApi.Core.MarkdownItems;
using System.Collections.Generic;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface IMarkdownItem
    {
        MarkdownProject Project { get; }

        MarkdownItemTypes ItemType { get; }

        TypeWrapper TypeInfo { get; }

        string Location { get; }

        string FileName { get; }

        string Name { get; }

        string FullName { get; }

        string Summary { get; }

        string BuildPage(ITheme theme);
    }
}