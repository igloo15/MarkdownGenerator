using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    internal interface IMarkdownable
    {
        string Name { get; }

        bool IsStatic { get; }
    }
}
