using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableProperty : IMarkdownable
    {
        public PropertyInfo InternalProperty { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalProperty.Name;

        public MarkdownableProperty(PropertyInfo info, bool isStatic)
        {
            InternalProperty = info;
            IsStatic = isStatic;
        }
    }
}
