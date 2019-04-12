# [Cleaner](./Cleaner.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../../README.md) > [Themes](./../README.md) > [Default](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
Used as a utility class for creating clean links and names for MarkdownItems

## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CleanFullName ( [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) t, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) keepGenericNumber, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) specialText ) | Cleans a type and returns the full name | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CleanName ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) name, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) keepGenericNumber, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) specialText ) | Cleans a name removing bad characters and other items | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CreateFullConstructorsWithLinks ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) currentItem, [`MarkdownConstructor`](./../../MarkdownItems/TypeParts/MarkdownConstructor.md) constructor, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useFullName, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useParameterNames ) | Create a constructor with links and parameters | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CreateFullMethodWithLinks ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) currentItem, [`MarkdownMethod`](./../../MarkdownItems/TypeParts/MarkdownMethod.md) method, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useFullName, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useParameterNames ) | Cleans a method and adds the appropiate links | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CreateFullParameterWithLinks ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) currentItem, [`MarkdownProperty`](./../../MarkdownItems/TypeParts/MarkdownProperty.md) property, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useFullName, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useParameterNames ) | Create a full parameter name with links | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | CreateFullTypeWithLinks ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) currentItem, [`Type`](https://docs.microsoft.com/en-us/dotnet/api/System.Type) target, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useFullName, [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) useSpecialText ) | Create a Link to the target from the MarkadownItem with full type information | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | RemoveGenerics ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) name ) | Remove generics from name basically the `1 text | 


