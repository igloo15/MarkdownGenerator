# [MarkdownNamespace](./MarkdownNamespace.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownNamespace.md)

## Summary
Markdown Namespace is a single namespace in a project

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [IMarkdownItem](./../Interfaces/IMarkdownItem.md)> | AllItems | Lookup of all the MarkdownItems in the project | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownEnum](./MarkdownEnum.md)> | Enums | The enums that exist at this Namespace | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [MarkdownProject](./MarkdownProject.md) | Project | The markdown project the item is part of | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownType](./MarkdownType.md)> | Types | The types that exist at this Namespace | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


