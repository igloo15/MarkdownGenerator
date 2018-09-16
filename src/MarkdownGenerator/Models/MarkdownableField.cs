using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableField : IMarkdownable
    {
        public FieldInfo InternalField { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalField.Name;

        public MarkdownableField(FieldInfo info, bool isStatic)
        {
            InternalField = info;
            IsStatic = isStatic;
        }
    }
}
