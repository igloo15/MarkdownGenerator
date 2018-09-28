# [DefaultTheme](./DefaultTheme.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Themes](./README.md)

Assembly: Igloo15.MarkdownApi.Core.dll

Implements [ITheme](./../Interfaces/ITheme.md)

## Summary
This default theme is bundled with the core to provide an example and base theming for documentation generated

## Constructors

| Name | Summary | 
| --- | --- | 
| DefaultTheme ( [`DefaultOptions`](./Default/DefaultOptions.md) options ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The name of this theme "Default" | 
| [IResolver](./../Interfaces/IResolver.md) | Resolver | The Default Resolver for this theme | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownNamespace`](./../MarkdownItems/MarkdownNamespace.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProject`](./../MarkdownItems/MarkdownProject.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownType`](./../MarkdownItems/MarkdownType.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEnum`](./../MarkdownItems/MarkdownEnum.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownField`](./../MarkdownItems/TypeParts/MarkdownField.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProperty`](./../MarkdownItems/TypeParts/MarkdownProperty.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownMethod`](./../MarkdownItems/TypeParts/MarkdownMethod.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEvent`](./../MarkdownItems/TypeParts/MarkdownEvent.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownConstructor`](./../MarkdownItems/TypeParts/MarkdownConstructor.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 


