using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    internal interface IMarkdownable
    {
        string Name { get; }

        bool IsStatic { get; }

        string GetLink();

        string GetName();

        string GetReturnOrType();

        string GetSummary();

        string GetCode();

        string GetDetailed();

        string GetExample();

        void Build(string destination, Options config);
    }
}
