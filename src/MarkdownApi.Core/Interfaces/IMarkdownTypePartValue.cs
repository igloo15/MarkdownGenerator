using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Interfaces
{
    public interface IMarkdownTypePartValue
    {
        string TypeName { get; }

        Type Type { get; }
    }
}
