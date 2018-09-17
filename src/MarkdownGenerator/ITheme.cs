using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    interface IThemePart<T>
    {
        ITheme RootTheme { get; }

        string GetName(T value);

        string GetLink(T value);

        string GetReturn(T value);

        string GetSummary(T value);

        string GetCode(T value);

        string GetDetailed(T value);

        string GetExample(T value);

        string[] GetTableHeaders();

        string GetPage(T value);
    }

    interface ITheme
    {
        string Name { get; }

        IThemePart<MarkdownableProject> ProjectPart { get; }

        IThemePart<MarkdownableNamespace> NamespacePart { get; }

        IThemePart<MarkdownableType> TypePart { get; }

        IThemePart<MarkdownableType> EnumPart { get; }

        IThemePart<MarkdownableMethod> MethodPart { get; }

        IThemePart<MarkdownableMethod> StaticMethodPart { get; }

        IThemePart<MarkdownableProperty> PropertyPart { get; }

        IThemePart<MarkdownableProperty> StaticPropertyPart { get; }

        IThemePart<MarkdownableField> FieldPart { get; }

        IThemePart<MarkdownableField> StaticFieldPart { get; }

        IThemePart<MarkdownableEvent> EventPart { get; }

        IThemePart<MarkdownableEvent> StaticEventPart { get; }
    }

}
