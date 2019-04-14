# [MarkdownType](./MarkdownType.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownType.md)

## Summary
A markdown type is a markdownitem containing a Type

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownType](./MarkdownType.md)> | GenericProperties | The Generic Properties for this MarkdownType | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType | The type of markdown item | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 
| [MarkdownConstructor](./MarkdownType.md)[] | GetConstructors ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) | Gets the MarkdownConstructors in this type | 
| [MarkdownEvent](./MarkdownType.md)[] | GetEvents ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) | Gets the MarkdownEvents in this type | 
| [MarkdownField](./MarkdownType.md)[] | GetFields ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) | Gets the MarkdownFields in this type | 
| [MarkdownMethod](./MarkdownType.md)[] | GetMethods ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) | Gets the MarkdownMethods in this type | 
| [MarkdownProperty](./MarkdownType.md)[] | GetProperties ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) | Gets the MarkdownProperties in this type | 


