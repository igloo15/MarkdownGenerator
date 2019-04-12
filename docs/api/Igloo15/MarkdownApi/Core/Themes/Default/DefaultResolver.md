# [DefaultResolver](./DefaultResolver.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../../README.md) > [Themes](./../README.md) > [Default](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IResolver](./../../Interfaces/IResolver.md)

## Summary
This default resolver is used to resolve the location to each page

## Constructors

| Name | Summary | 
| --- | --- | 
| DefaultResolver ( [`DefaultOptions`](./DefaultOptions.md) options ) | Constructs the Default Resolver using the given options | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | GetFileName ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) item ) | Gets the filename for the markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | GetPath ( [`IMarkdownItem`](./../../Interfaces/IMarkdownItem.md) item ) | Calculates the path for the markdown item | 


