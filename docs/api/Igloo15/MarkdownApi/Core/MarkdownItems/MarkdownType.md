# [MarkdownType](./MarkdownType.md)

Namespace: [Igloo15]() > [MarkdownApi]() > [Core](./../README.md) > [MarkdownItems](./README.md)

Assembly: igloo15.MarkdownApi.Core.dll

Implements [IMarkdownItem](./../Interfaces/IMarkdownItem.md), [IInternalMarkdownItem]()


## Constructors

| Name | Summary | 
| --- | --- | 
| MarkdownType (  ) |  | 


## Properties

| Type | Name | Summary | 
| --- | --- | --- | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownConstructor](./TypeParts/MarkdownConstructor.md)> | Constructors |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownEvent](./TypeParts/MarkdownEvent.md)> | Events |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownField](./TypeParts/MarkdownField.md)> | Fields |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | FullName |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownType](./MarkdownType.md)> | GenericProperties |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[String](https://docs.microsoft.com/en-us/dotnet/api/System.String)> | Interfaces |  | 
| [Type](https://docs.microsoft.com/en-us/dotnet/api/System.Type) | InternalType |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsAbstract |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsGeneric |  | 
| [Boolean](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) | IsInterface |  | 
| [MarkdownItemTypes](./../MarkdownItemTypes.md) | ItemType |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownMethod](./TypeParts/MarkdownMethod.md)> | Methods |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Name |  | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | Namespace |  | 
| [MarkdownNamespace](./MarkdownNamespace.md) | NamespaceItem |  | 
| [MarkdownProject](./MarkdownProject.md) | Project |  | 
| [List](https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.List-1)\<[MarkdownProperty](./TypeParts/MarkdownProperty.md)> | Properties |  | 
| [TypeWrapper](./../TypeWrapper.md) | TypeInfo |  | 


## Methods

| Return | Name | Summary | 
| --- | --- | --- | 
| [String](https://docs.microsoft.com/en-us/dotnet/api/System.String) | BuildPage ( [`ITheme`](./../Interfaces/ITheme.md) theme ) |  | 
| [MarkdownConstructor]()[] | GetConstructors ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) |  | 
| [MarkdownEvent]()[] | GetEvents ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) |  | 
| [MarkdownField]()[] | GetFields ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) |  | 
| [MarkdownMethod]()[] | GetMethods ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) |  | 
| [MarkdownProperty]()[] | GetProperties ( [`Boolean`](https://docs.microsoft.com/en-us/dotnet/api/System.Boolean) isStatic ) |  | 


