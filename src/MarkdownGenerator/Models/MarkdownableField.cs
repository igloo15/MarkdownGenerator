using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableField : IMarkdownable
    {
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        public FieldInfo InternalField { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalField.Name;

        public Options Config { get; private set; }

        public MarkdownableField(FieldInfo info, bool isStatic, IEnumerable<XmlDocumentComment> comments)
        {
            InternalField = info;
            IsStatic = isStatic;
        }

        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = InternalField.GetFilePath(destination);
            
        }
    }
}
