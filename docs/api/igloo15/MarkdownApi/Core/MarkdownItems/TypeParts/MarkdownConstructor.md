# [MarkdownConstructor](./MarkdownConstructor.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../../README.md) > [MarkdownItems](./../README.md) > [TypeParts](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownConstructor.md)

## Summary
Markdown Constructor is a TypePart identified as the construct of a Markdown Type

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [ConstructorInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.ConstructorInfo) | InternalItem | The ConstructorInfo for this Markdown Constructor | 
| [MarkdownItemTypes](./../../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [TypeWrapper](./../../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


