# [MarkdownProject](./MarkdownProject.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md)


## Constructors

| Name | Summary | 
| --- | --- | 
| MarkdownProject (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [Dictionary](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.Dictionary-2)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String), [IMarkdownItem](./../Interfaces/IMarkdownItem.md)> | AllItems |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName |  | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name |  | 
| [MarkdownProject](./MarkdownProject.md) | Project |  | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| void | Build ( [`ITheme`](./../Interfaces/ITheme.md) theme, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) outputLocation ) |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) |  | 
| [MarkdownProject](./MarkdownProject.md) | Create ( [`ITheme`](./../Interfaces/ITheme.md) theme, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) outputLocation ) |  | 
| [MarkdownProject](./MarkdownProject.md) | Resolve ( [`ITheme`](./../Interfaces/ITheme.md) theme ) |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | TryGetValue ( [`TypeWrapper`](./../TypeWrapper.md) wrapper, [`IMarkdownItem&`]() value ) |  | 


