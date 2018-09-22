using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    internal interface IMarkdownable
    {
        string FolderPath { get; }

        string FilePath { get; }

        string Name { get; }

        bool IsStatic { get; }

        Options Config { get; }

        MemberInfo Info { get; }

        void Build(string destination, Options config);
    }
}
