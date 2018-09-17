using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableEvent : IMarkdownable
    {
        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        public EventInfo InternalEvent { get; private set; }

        public bool IsStatic { get; private set; }

        public string Name => InternalEvent.Name;

        public Options Config { get; private set; }

        public MarkdownableEvent(EventInfo info, bool isStatic, IEnumerable<XmlDocumentComment> comments)
        {
            InternalEvent = info;
            IsStatic = isStatic;
        }

        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = InternalEvent.GetFilePath(destination);
        }
    }
}
