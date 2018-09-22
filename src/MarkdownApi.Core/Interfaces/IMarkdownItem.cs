namespace Igloo15.MarkdownApi.Core
{
    public interface IMarkdownItem
    {
        MarkdownItemTypes ItemType { get; }

        string Location { get; }

        string FileName { get; }

        string Name { get; }

        string FullName { get; }
    }
}