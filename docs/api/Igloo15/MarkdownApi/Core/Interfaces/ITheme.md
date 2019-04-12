# [ITheme](./ITheme.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Interfaces](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
A theme is used to generate the content for markdown pages as well as where markdown pages should be placed

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The name of the Theme | 
| [IResolver](./IResolver.md) | Resolver | Resolves filepaths and filenames for all Markdown Items | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownNamespace`](./../MarkdownItems/MarkdownNamespace.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProject`](./../MarkdownItems/MarkdownProject.md) item ) | Builds Project Pages with the given MarkdownProject Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownType`](./../MarkdownItems/MarkdownType.md) item ) | Builds Type Pages with the given MarkdownType Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEnum`](./../MarkdownItems/MarkdownEnum.md) item ) | Builds Enum Pages with the given MarkdownEnum Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownField`](./../MarkdownItems/TypeParts/MarkdownField.md) item ) | Builds Field Pages with the given MarkdownField Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProperty`](./../MarkdownItems/TypeParts/MarkdownProperty.md) item ) | Builds Property Pages with the given MarkdownProperty Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownMethod`](./../MarkdownItems/TypeParts/MarkdownMethod.md) item ) | Builds Method Pages with the given MarkdownMethod Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEvent`](./../MarkdownItems/TypeParts/MarkdownEvent.md) item ) | Builds Event Pages with the given MarkdownEvent Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownConstructor`](./../MarkdownItems/TypeParts/MarkdownConstructor.md) item ) | Builds Constructor Pages with the given MarkdownConstructor Item | 
| void | SetLogger ( [`ILogger`](./ITheme.md) logger ) | Set the default logger to be used during Theme Construction | 


