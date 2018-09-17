using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableProperty : IMarkdownable
    {
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        public PropertyInfo InternalProperty { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalProperty.Name;

        public Options Config { get; private set; }

        public MarkdownableProperty(PropertyInfo info, bool isStatic, IEnumerable<XmlDocumentComment> comments)
        {
            InternalProperty = info;
            IsStatic = isStatic;
        }

        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = InternalProperty.GetFilePath(destination);
            
        }
    }
}
