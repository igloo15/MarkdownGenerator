# [MarkdownBuilder](./MarkdownBuilder.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Builders](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll


## Constructors

| Name | Summary | 
| --- | --- | 
| MarkdownBuilder (  ) |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Append ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | AppendLine (  ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | AppendLine ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Code ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) language, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | CodeQuote ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Header ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | HeaderWithCode ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | HeaderWithLink ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Image ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) altText, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) imageUrl ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Link ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | List ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | ListLink ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) |  | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Table ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)[] headers, [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)[]> items ) | Create a table with the header and given items | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) |  | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | MarkdownCodeQuote ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) |  | 


