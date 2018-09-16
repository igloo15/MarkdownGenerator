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

        public string GetLink()
        {
            throw new NotImplementedException();
        }

        public string GetName()
        {
            throw new NotImplementedException();
        }

        public string GetReturn()
        {
            throw new NotImplementedException();
        }

        public string GetSummary()
        {
            throw new NotImplementedException();
        }

        public string GetCode()
        {
            throw new NotImplementedException();
        }

        public string GetDetailed()
        {
            throw new NotImplementedException();
        }

        public string GetExample()
        {
            throw new NotImplementedException();
        }

        public string BuildPage()
        {
            throw new NotImplementedException();
        }

        public void Build(string destination)
        {
            throw new NotImplementedException();
        }
    }
}
