# [AbstractMarkdownItem](./AbstractMarkdownItem.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./AbstractMarkdownItem.md)

## Summary
The abstract Markdown item that implements some base properties and functions for MarkdownItems

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FileName | The Filename of the markdown page | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Location | The location of the markdown page for this item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [MarkdownProject](./MarkdownProject.md) | Project | The markdown project the item is part of | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Summary | The summary of the markdown item | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


