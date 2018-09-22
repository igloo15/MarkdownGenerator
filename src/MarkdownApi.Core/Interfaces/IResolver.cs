using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface IResolver
    {
        string GetPath(IMarkdownItem item);

        string GetFileName(IMarkdownItem item);
    }
}
