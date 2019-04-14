# [AbstractType](./AbstractType.md)

Namespace: [igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem](./AbstractType.md)

## Summary
AbstractType implements base functionality for MarkdownType and MarkdownEnum

## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [IEnumerable](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.IEnumerable-1)\<[XmlDocumentComment](./../Builders/XmlDocumentComment.md)> | Comments | The comments related to Type and TypeParts | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName | The full name of the Markdown Item | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)> | Interfaces | THe interfaces this MarkdownType implements | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | InternalType | The type information for this MarkdownType | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsAbstract | Determines if type is abstract | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsGeneric | Determines if Type is Generic | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsInterface | Determines if type is interface | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name | The Name of the Markdown item | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Namespace | The namespace this item exists in | 
| [MarkdownNamespace](./MarkdownNamespace.md) | NamespaceItem | The name space this markdown is in | 
| [MarkdownProject](./MarkdownProject.md) | Project | The markdown project the item is part of | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo | The type info of the MarkdownItem used to find references to it from other MarkdownItems | 


