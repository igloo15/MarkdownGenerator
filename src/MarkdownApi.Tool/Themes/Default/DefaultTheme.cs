using System;
using System.Collections.Generic;
using System.Text;
using Igloo15.MarkdownGenerator.Models;

namespace Igloo15.MarkdownGenerator.Themes.Default
{
    internal class DefaultTheme : ITheme
    {

        public string Name => "Default";

        public IThemePart<MarkdownableProject> ProjectPart => new DefaultProjectPart(this);

        public IThemePart<MarkdownableNamespace> NamespacePart => new DefaultNamespacePart(this);

        public IThemePart<MarkdownableType> TypePart => new DefaultTypePart(this);

        public IThemePart<MarkdownableType> EnumPart => new DefaultEnumPart(this);

        public IThemePart<MarkdownableMethod> MethodPart => new DefaultMethodPart(this);

        public IThemePart<MarkdownableMethod> StaticMethodPart => new DefaultMethodPart(this);

        public IThemePart<MarkdownableProperty> PropertyPart => new DefaultPropertyPart(this);

        public IThemePart<MarkdownableProperty> StaticPropertyPart => new DefaultPropertyPart(this);

        public IThemePart<MarkdownableField> FieldPart => new DefaultFieldPart(this);

        public IThemePart<MarkdownableField> StaticFieldPart => new DefaultFieldPart(this);

        public IThemePart<MarkdownableEvent> EventPart => new DefaultEventPart(this);

        public IThemePart<MarkdownableEvent> StaticEventPart => new DefaultEventPart(this);
    }
}
