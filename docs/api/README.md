# [API](./README.md)

## Summary
This is the root summary

## Namespaces

### [igloo15.MarkdownApi.Core](./igloo15/MarkdownApi/Core/README.md)

- [`MarkdownApiGenerator`](./igloo15/MarkdownApi/Core/MarkdownApiGenerator.md)
	- The main class used to generate a project based on the search area
- [`PublicExtensions`](./igloo15/MarkdownApi/Core/PublicExtensions.md)
	- Public Extensions are extension methods meant to be used by anyone with dependency on the MarkdownApi.Core
- [`TypeWrapper`](./igloo15/MarkdownApi/Core/TypeWrapper.md)
	- A wrapper around a type
### [igloo15.MarkdownApi.Core.Themes](./igloo15/MarkdownApi/Core/Themes/README.md)

- [`DefaultTheme`](./igloo15/MarkdownApi/Core/Themes/DefaultTheme.md)
	- This default theme is bundled with the core to provide an example and base theming for documentation generated
### [igloo15.MarkdownApi.Core.Themes.Default](./igloo15/MarkdownApi/Core/Themes/Default/README.md)

- [`Cleaner`](./igloo15/MarkdownApi/Core/Themes/Default/Cleaner.md)
	- Used as a utility class for creating clean links and names for MarkdownItems
- [`DefaultConstructorBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultConstructorBuilder.md)
	- The default markdown constructor page builder - Warning this is not yet implemented
- [`DefaultEnumBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultEnumBuilder.md)
	- The default enum page builder
- [`DefaultMethodBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultMethodBuilder.md)
	- Default markdown method page builder - Warning this is not yet implemented
- [`DefaultNamespaceBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultNamespaceBuilder.md)
	- The markdown namespace page builder
- [`DefaultOptions`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultOptions.md)
	- The default options that can be set on the default theme
- [`DefaultProjectBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultProjectBuilder.md)
	- A markdown project page builder this is the root of all the markdown api content
- [`DefaultResolver`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultResolver.md)
	- This default resolver is used to resolve the location to each page
- [`DefaultTypeBuilder`](./igloo15/MarkdownApi/Core/Themes/Default/DefaultTypeBuilder.md)
	- The default type page builder
### [igloo15.MarkdownApi.Core.MarkdownItems](./igloo15/MarkdownApi/Core/MarkdownItems/README.md)

- [`AbstractMarkdownItem`](./igloo15/MarkdownApi/Core/MarkdownItems/AbstractMarkdownItem.md)
	- The abstract Markdown item that implements some base properties and functions for MarkdownItems
- [`AbstractType`](./igloo15/MarkdownApi/Core/MarkdownItems/AbstractType.md)
	- AbstractType implements base functionality for MarkdownType and MarkdownEnum
- [`MarkdownEnum`](./igloo15/MarkdownApi/Core/MarkdownItems/MarkdownEnum.md)
	- Markdown Enum is a type that is a Enum
- [`MarkdownNamespace`](./igloo15/MarkdownApi/Core/MarkdownItems/MarkdownNamespace.md)
	- Markdown Namespace is a single namespace in a project
- [`MarkdownProject`](./igloo15/MarkdownApi/Core/MarkdownItems/MarkdownProject.md)
	- A markdown project used to produce markdown files
- [`MarkdownType`](./igloo15/MarkdownApi/Core/MarkdownItems/MarkdownType.md)
	- A markdown type is a markdownitem containing a Type
### [igloo15.MarkdownApi.Core.MarkdownItems.TypeParts](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/README.md)

- [`AbstractTypePart`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/AbstractTypePart.md)
	- Abstract Markdown Type Part used to implement basic MarkdownTypePart functionality
- [`MarkdownConstructor`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownConstructor.md)
	- Markdown Constructor is a TypePart identified as the construct of a Markdown Type
- [`MarkdownEvent`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownEvent.md)
	- MarkdownEvent is an event in a MarkdownType
- [`MarkdownField`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownField.md)
	- The MarkdownField is a field info that is part of  MarkdownType
- [`MarkdownMethod`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownMethod.md)
	- MarkdownMethod represents the method info of a MarkdownType
- [`MarkdownProperty`](./igloo15/MarkdownApi/Core/MarkdownItems/TypeParts/MarkdownProperty.md)
	- MarkdownProperty represents the propertyInfo in the MarkdownType
### [igloo15.MarkdownApi.Core.Interfaces](./igloo15/MarkdownApi/Core/Interfaces/README.md)

- [`IMarkdownItem`](./igloo15/MarkdownApi/Core/Interfaces/IMarkdownItem.md)
	- A markdown item that could be parsed to a markdown page
- [`IMarkdownTypePartValue`](./igloo15/MarkdownApi/Core/Interfaces/IMarkdownTypePartValue.md)
	- A Markdown Type Part basically this is a part of a MarkdownType
- [`IResolver`](./igloo15/MarkdownApi/Core/Interfaces/IResolver.md)
	- Resolves the path/folder of the markdown item and gets a file name
- [`ITheme`](./igloo15/MarkdownApi/Core/Interfaces/ITheme.md)
	- A theme is used to generate the content for markdown pages as well as where markdown pages should be placed
### [igloo15.MarkdownApi.Core.Builders](./igloo15/MarkdownApi/Core/Builders/README.md)

- [`MarkdownBuilder`](./igloo15/MarkdownApi/Core/Builders/MarkdownBuilder.md)
	- Builds Markdown strings
- [`XmlDocumentComment`](./igloo15/MarkdownApi/Core/Builders/XmlDocumentComment.md)
	- Xml Comment in Xml Document

