# [ITheme](./ITheme.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Interfaces](./README.md)

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
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProject`](./../MarkdownItems/MarkdownProject.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownType`](./../MarkdownItems/MarkdownType.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEnum`](./../MarkdownItems/MarkdownEnum.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownField`](./../MarkdownItems/TypeParts/MarkdownField.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownProperty`](./../MarkdownItems/TypeParts/MarkdownProperty.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownMethod`](./../MarkdownItems/TypeParts/MarkdownMethod.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownEvent`](./../MarkdownItems/TypeParts/MarkdownEvent.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`MarkdownConstructor`](./../MarkdownItems/TypeParts/MarkdownConstructor.md) item ) | Builds Namespace Pages with the given MarkdownNamespace Item | 
| void | SetLogger ( [`ILogger`](./ITheme.md) logger ) | Set the default logger to be used during Theme Construction | 


