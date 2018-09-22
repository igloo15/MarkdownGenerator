namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface IMarkdownItem
    {
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