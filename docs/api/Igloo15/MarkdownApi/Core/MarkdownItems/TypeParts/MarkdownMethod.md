# [MarkdownMethod](./MarkdownMethod.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../../README.md) > [MarkdownItems](./../README.md) > [TypeParts](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./MarkdownMethod.md)

## Summary
MarkdownMethod represents the method info of a MarkdownType

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | BaseDefinition | Determines what the type is that has the base definition of this method | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [MethodInfo](https://docs.microsoft.com/en-us/dotnet/api/System.Reflection.MethodInfo) | InternalItem | The Method Info for this markdown item | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsAbstract | Determines if method is abstract | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsOverriden | Determines if method is overriden | 
| [MarkdownItemTypes](./../../MarkdownItemTypes.md) | ItemType | The type of markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | ReturnType | Determines what the return type is for this method | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ReturnTypeName | The name of the return type for this method | 
| [TypeWrapper](./../../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../../Interfaces/ITheme.md) theme ) | Create a page for this markdown item or "" if no page is created | 


