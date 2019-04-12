# [MarkdownBuilder](./MarkdownBuilder.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [Builders](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

## Summary
Builds Markdown strings

## Constructors

| Name | Summary | 
| --- | --- | 
| MarkdownBuilder (  ) |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Append ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) | Appends text to an internal string builder | 
| [MarkdownBuilder](./MarkdownBuilder.md) | AppendLine (  ) | Appends a new line to the internal string builder | 
| [MarkdownBuilder](./MarkdownBuilder.md) | AppendLine ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) | Appends text and a new line to internal string builder | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Code ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) language, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) | Wrap text in a code block for a specific language | 
| [MarkdownBuilder](./MarkdownBuilder.md) | CodeQuote ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) | Single Code Quote | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Header ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) | Create a header at the given level with the text | 
| [MarkdownBuilder](./MarkdownBuilder.md) | HeaderWithCode ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) | Header with code | 
| [MarkdownBuilder](./MarkdownBuilder.md) | HeaderWithLink ( [`Int32`](https://docs.microsoft.com/en-us/dotnet/api/System.Int32) level, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) | The header with a link | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Image ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) altText, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) imageUrl ) | Create a markdown image | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Link ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) | A markdown link | 
| [MarkdownBuilder](./MarkdownBuilder.md) | List ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text ) | Creates a Markdown List | 
| [MarkdownBuilder](./MarkdownBuilder.md) | ListLink ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) text, [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) url ) | Create a link on a list item | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Tab (  ) | Appends a Tab to the current line | 
| [MarkdownBuilder](./MarkdownBuilder.md) | Table ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)[] headers, [`IEnumerable`](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String)[]> items ) | Create a table with the header and given items | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | ToString (  ) | Convert internal stringbuilder to string | 


## Static Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | MarkdownCodeQuote ( [`String`](https://docs.microsoft.com/en-us/dotnet/api/System.String) code ) | Places code in a markdown codeblock | 


