# [IMarkdownItem](./IMarkdownItem.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Interfaces](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
A markdown item that could be parsed to a markdown page

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FileName | The Filename of the markdown page | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Location | The location of the markdown page for this item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [MarkdownProject](./../MarkdownItems/MarkdownProject.md) | Project | The markdown project the item is part of | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Summary | The summary of the markdown item | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


