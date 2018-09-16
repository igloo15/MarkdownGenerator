using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableMethod : IMarkdownable
    {
        public MethodInfo InternalMethod { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalMethod.Name;

        public MarkdownableMethod(MethodInfo info, bool isStatic)
        {
            InternalMethod = info;
            IsStatic = isStatic;
        }
    }
}
