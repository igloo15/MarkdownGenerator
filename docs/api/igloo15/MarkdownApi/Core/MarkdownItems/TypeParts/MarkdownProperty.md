# [MarkdownProperty](./MarkdownProperty.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../../README.md) > [MarkdownItems](./../README.md) > [TypeParts](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownProperty.md), [IMarkdownTypePartValue](./../../Interfaces/IMarkdownTypePartValue.md)

## Summary
MarkdownProperty represents the propertyInfo in the MarkdownType

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [PropertyInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.PropertyInfo) | InternalItem | The property info of this MarkdownProperty | 
| [MarkdownItemTypes](./../../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | Type | The Type the part | 
| [TypeWrapper](./../../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | TypeName | The type name | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


