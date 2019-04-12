# [MarkdownProject](./MarkdownProject.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownProject.md)

## Summary
A markdown project used to produce markdown files

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [IMarkdownItem](./../Interfaces/IMarkdownItem.md)> | AllItems | All the renderable markdown items | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [MarkdownProject](./MarkdownProject.md) | Project | The markdown project the item is part of | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Build ( [`ITheme`](./../Interfaces/ITheme.md) theme, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) outputLocation ) | Build the Markdown Project and all items in the project with the given field in the given location. This method is shorthand for calling Resolve and then Create | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 
| [MarkdownProject](./MarkdownProject.md) | Create ( [`ITheme`](./../Interfaces/ITheme.md) theme, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) outputLocation ) | Create the Markdown Api Pages based on the ITheme provided and put all files in the given location | 
| [MarkdownProject](./MarkdownProject.md) | Resolve ( [`ITheme`](./../Interfaces/ITheme.md) theme ) | Resolve the file names and locations based on the ITheme provided | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | TryGetValue ( [`TypeWrapper`](./../TypeWrapper.md) wrapper, out [`IMarkdownItem`](./MarkdownProject.md) value ) | Try to get a markdownitem with the given TypeWrapper lookup key | 


