using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Interfaces
{
    public interface IThemePart<T>
    {
        string GetName(T value);

        string GetLink(T value);

        string GetReturn(T value);

        string GetSummary(T value);

        string GetCode(T value);

        string GetDetailed(T value);

        string GetExample(T value);
    }

    public interface ITheme
    {
        IThemePart<IMarkdownableNamespace> NamespacePart { get; }

        IThemePart<IMarkdownableType> TypePart { get; }

        IThemePart<IMarkdownableType> EnumPart { get; }

        IThemePart<IMarkdownableMethod> MethodPart { get; }

        IThemePart<IMarkdownableMethod> StaticMethodPart { get; }

        IThemePart<IMarkdownableProperty> PropertyPart { get; }

        IThemePart<IMarkdownableProperty> StaticPropertyPart { get; }

        IThemePart<IMarkdownableField> FieldPart { get; }

        IThemePart<IMarkdownableField> StaticFieldPart { get; }

        IThemePart<IMarkdownableEvent> EventPart { get; }

        IThemePart<IMarkdownableEvent> StaticEventPart { get; }
    }

}
