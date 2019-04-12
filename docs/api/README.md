# [API](./README.md)

## Summary
This is the root summary

## Namespaces

### [Igloo15.MarkdownApi.Core](./Igloo15/MarkdownApi/Core/README.md)

- [`MarkdownApiGenerator`](./Igloo15/MarkdownApi/Core/MarkdownApiGenerator.md)
	- The main class used to generate a project based on the search area
- [`PublicExtensions`](./Igloo15/MarkdownApi/Core/PublicExtensions.md)
	- Public Extensions are extension methods meant to be used by anyone with dependency on the MarkdownApi.Core
- [`TypeWrapper`](./Igloo15/MarkdownApi/Core/TypeWrapper.md)
	- A wrapper around a type
### [Igloo15.MarkdownApi.Core.Themes](./Igloo15/MarkdownApi/Core/Themes/README.md)

- [`DefaultTheme`](./Igloo15/MarkdownApi/Core/Themes/DefaultTheme.md)
	- This default theme is bundled with the core to provide an example and base theming for documentation generated
### [Igloo15.MarkdownApi.Core.Themes.Default](./Igloo15/MarkdownApi/Core/Themes/Default/README.md)

- [`Cleaner`](./Igloo15/MarkdownApi/Core/Themes/Default/Cleaner.md)
	- Used as a utility class for creating clean links and names for MarkdownItems
- [`DefaultConstructorBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultConstructorBuilder.md)
	- The default markdown constructor page builder - Warning this is not yet implemented
- [`DefaultEnumBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultEnumBuilder.md)
	- The default enum page builder
- [`DefaultMethodBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultMethodBuilder.md)
	- Default markdown method page builder - Warning this is not yet implemented
- [`DefaultNamespaceBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultNamespaceBuilder.md)
	- The markdown namespace page builder
- [`DefaultOptions`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultOptions.md)
	- The default options that can be set on the default theme
- [`DefaultProjectBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultProjectBuilder.md)
	- A markdown project page builder this is the root of all the markdown api content
- [`DefaultResolver`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultResolver.md)
	- This default resolver is used to resolve the location to each page
- [`DefaultTypeBuilder`](./Igloo15/MarkdownApi/Core/Themes/Default/DefaultTypeBuilder.md)
	- The default type page builder
### [Igloo15.MarkdownApi.Core.MarkdownItems](./Igloo15/MarkdownApi/Core/MarkdownItems/README.md)

- [`AbstractMarkdownItem`](./Igloo15/MarkdownApi/Core/MarkdownItems/AbstractMarkdownItem.md)
	- The abstract Markdown item that implements some base properties and functions for MarkdownItems
- [`AbstractType`](./Igloo15/MarkdownApi/Core/MarkdownItems/AbstractType.md)
	- AbstractType implements base functionality for MarkdownType and MarkdownEnum
- [`MarkdownEnum`](./Igloo15/MarkdownApi/Core/MarkdownItems/MarkdownEnum.md)
	- Markdown Enum is a type that is a Enum
- [`MarkdownNamespace`](./Igloo15/MarkdownApi/Core/MarkdownItems/MarkdownNamespace.md)
	- Markdown Namespace is a single namespace in a project
- [`MarkdownProject`](./Igloo15/MarkdownApi/Core/MarkdownItems/MarkdownProject.md)
	- A markdown project used to produce markdown files
- [`MarkdownType`](./Igloo15/MarkdownApi/Core/MarkdownItems/MarkdownType.md)
	- A markdown type is a markdownitem containing a Type
### [Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/README.md)

- [`AbstractTypePart`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/AbstractTypePart.md)
	- Abstract Markdown Type Part used to implement basic MarkdownTypePart functionality
- [`MarkdownConstructor`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownConstructor.md)
	- Markdown Constructor is a TypePart identified as the construct of a Markdown Type
- [`MarkdownEvent`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownEvent.md)
	- MarkdownEvent is an event in a MarkdownType
- [`MarkdownField`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownField.md)
	- The MarkdownField is a field info that is part of  MarkdownType
- [`MarkdownMethod`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownMethod.md)
	- MarkdownMethod represents the method info of a MarkdownType
- [`MarkdownProperty`](./Igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownProperty.md)
	- MarkdownProperty represents the propertyInfo in the MarkdownType
### [Igloo15.MarkdownApi.Core.Interfaces](./Igloo15/MarkdownApi/Core/Interfaces/README.md)

- [`IMarkdownItem`](./Igloo15/MarkdownApi/Core/Interfaces/IMarkdownItem.md)
	- A markdown item that could be parsed to a markdown page
- [`IMarkdownTypePartValue`](./Igloo15/MarkdownApi/Core/Interfaces/IMarkdownTypePartValue.md)
	- A Markdown Type Part basically this is a part of a MarkdownType
- [`IResolver`](./Igloo15/MarkdownApi/Core/Interfaces/IResolver.md)
	- Resolves the path/folder of the markdown item and gets a file name
- [`ITheme`](./Igloo15/MarkdownApi/Core/Interfaces/ITheme.md)
	- A theme is used to generate the content for markdown pages as well as where markdown pages should be placed
### [Igloo15.MarkdownApi.Core.Builders](./Igloo15/MarkdownApi/Core/Builders/README.md)

- [`MarkdownBuilder`](./Igloo15/MarkdownApi/Core/Builders/MarkdownBuilder.md)
	- Builds Markdown strings
- [`XmlDocumentComment`](./Igloo15/MarkdownApi/Core/Builders/XmlDocumentComment.md)
	- Xml Comment in Xml Document

