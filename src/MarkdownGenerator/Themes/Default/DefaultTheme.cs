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

        public IThemePart<MarkdownableMethod> StaticMethodPart => throw new NotImplementedException();

        public IThemePart<MarkdownableProperty> PropertyPart => throw new NotImplementedException();

        public IThemePart<MarkdownableProperty> StaticPropertyPart => throw new NotImplementedException();

        public IThemePart<MarkdownableField> FieldPart => throw new NotImplementedException();

        public IThemePart<MarkdownableField> StaticFieldPart => throw new NotImplementedException();

        public IThemePart<MarkdownableEvent> EventPart => throw new NotImplementedException();

        public IThemePart<MarkdownableEvent> StaticEventPart => throw new NotImplementedException();
    }
}
