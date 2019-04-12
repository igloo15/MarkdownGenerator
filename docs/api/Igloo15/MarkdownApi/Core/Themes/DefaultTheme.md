# [DefaultTheme](./DefaultTheme.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Themes](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [ITheme](./../Interfaces/ITheme.md)

## Summary
This default theme is bundled with the core to provide an example and base theming for documentation generated

## Constructors

| Name | Summary | 
| --- | --- | 
| DefaultTheme ( [`DefaultOptions`](./Default/DefaultOptions.md) options ) | Constructs a default theme with the given options | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The name of this theme "Default" | 
| [IResolver](./../Interfaces/IResolver.md) | Resolver | The Default Resolver for this theme | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownNamespace`](./../MarkdownItems/MarkdownNamespace.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProject`](./../MarkdownItems/MarkdownProject.md) item ) | Builds Project Pages with the given MarkdownProject Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownType`](./../MarkdownItems/MarkdownType.md) item ) | Builds Type Pages with the given MarkdownType Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEnum`](./../MarkdownItems/MarkdownEnum.md) item ) | Builds Enum Pages with the given MarkdownEnum Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownField`](./../MarkdownItems/TypeParts/MarkdownField.md) item ) | In default theme this returns only "" | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProperty`](./../MarkdownItems/TypeParts/MarkdownProperty.md) item ) | In default theme this returns only "" | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownMethod`](./../MarkdownItems/TypeParts/MarkdownMethod.md) item ) | In default theme this returns only "" | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEvent`](./../MarkdownItems/TypeParts/MarkdownEvent.md) item ) | In default theme this returns only "" | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownConstructor`](./../MarkdownItems/TypeParts/MarkdownConstructor.md) item ) | In default theme this returns only "" | 
| void | SetLogger ( [`ILogger`](./DefaultTheme.md) logger ) | Set the default logger to be used during Theme Construction | 


