using System.Collections.Generic;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface IMarkdownItem
    {
        Dictionary<string, IMarkdownItem> AllItems { get; }

        MarkdownItemTypes ItemType { get; }

        string Location { get; }

        string FileName { get; }

        string Name { get; }

        string FullName { get; }

        string Summary { get; }

        string GetId();

        string BuildPage(ITheme theme);
    }
}