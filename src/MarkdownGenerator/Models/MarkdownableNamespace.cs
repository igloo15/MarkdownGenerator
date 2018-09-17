using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableNamespace : IMarkdownable
    {
        public string FullName { get; private set; }

        public string FolderPath { get; private set; }
        public string FilePath { get; private set; }

        public List<MarkdownableType> Types { get; private set; }

        public string Name => FullName;

        public bool IsStatic => false;

        public Options Config { get; private set; }

        public MarkdownableNamespace(List<MarkdownableType> types, string fullName)
        {
            FullName = fullName;
            Types = types;
        }
                
        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = Path.Combine(FolderPath, $"{Config.RootFileName}.md");

            if (!Directory.Exists(FolderPath))
                Directory.CreateDirectory(FolderPath);

            foreach (var item in Types.OrderBy(x => x.Name))
            {
                item.Build(FolderPath, Config);
            }

            if (Config.NamespacePages)
            {
                var content = Config.CurrentTheme.NamespacePart.GetPage(this);
                File.WriteAllText(FilePath, content);
            }

            
        }
    }
}
