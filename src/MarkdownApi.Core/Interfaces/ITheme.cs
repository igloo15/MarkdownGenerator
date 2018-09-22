using Igloo15.MarkdownApi.Core.TypeParts;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface ITheme
    {
        string RootName { get; }

        IResolver Resolver { get; }

        string BuildPage(MarkdownNamespace item);

        string BuildPage(MarkdownProject item);

        string BuildPage(MarkdownType item);

        string BuildPage(MarkdownEnum item);

        string BuildPage(MarkdownField item);

        string BuildPage(MarkdownProperty item);

        string BuildPage(MarkdownMethod item);

        string BuildPage(MarkdownEvent item);
    }
}
