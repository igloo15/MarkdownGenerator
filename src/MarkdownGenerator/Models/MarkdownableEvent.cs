using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableEvent : IMarkdownable
    {
        public EventInfo InternalEvent { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalEvent.Name;

        public MarkdownableEvent(EventInfo info, bool isStatic)
        {
            InternalEvent = info;
            IsStatic = isStatic;
        }
    }
}
