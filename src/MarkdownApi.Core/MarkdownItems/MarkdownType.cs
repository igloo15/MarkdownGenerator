using System;
using System.Collections.Generic;
using System.Linq;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public class MarkdownType : AbstractType
    {
        
        internal MarkdownType() { }

        internal List<MarkdownProperty> Properties { get; } = new List<MarkdownProperty>();

        internal List<MarkdownField> Fields { get; } = new List<MarkdownField>();

        internal List<MarkdownEvent> Events { get; } = new List<MarkdownEvent>();

        internal List<MarkdownMethod> Methods { get; } = new List<MarkdownMethod>();

        internal List<MarkdownConstructor> Constructors { get; } = new List<MarkdownConstructor>();


        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Type;

        public List<MarkdownType> GenericProperties { get; } = new List<MarkdownType>();

        public MarkdownField[] GetFields(bool isStatic) => Fields.Where(f => f.IsStatic == isStatic).ToArray();

        public MarkdownProperty[] GetProperties(bool isStatic) => Properties.Where(f => f.IsStatic == isStatic).ToArray();

        public MarkdownMethod[] GetMethods(bool isStatic) => Methods.Where(f => f.IsStatic == isStatic).ToArray();

        public MarkdownEvent[] GetEvents(bool isStatic) => Events.Where(f => f.IsStatic == isStatic).ToArray();

        public MarkdownConstructor[] GetConstructors(bool isStatic) => Constructors.Where(c => c.IsStatic == isStatic).ToArray();

        public override string BuildPage(ITheme theme) => theme.BuildPage(this);

    }
}