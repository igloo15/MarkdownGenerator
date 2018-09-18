using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableProject : IMarkdownable
    {
        public string FolderPath { get; private set; }

        public string FilePath { get; private set; }

        public string Name { get; private set; }

        public bool IsStatic => false;

        public Options Config { get; private set; }

        public MarkdownableNamespace[] Namespaces { get; private set; }

        public MemberInfo Info => null;

        public MarkdownableProject(MarkdownableNamespace[] namespaces, Options config)
        {
            Config = config;
            Name = config.RootTitle;
            FolderPath = config.Destination;
            Namespaces = namespaces;
            Config = config;
        }
        
        public void Build(string destination, Options config)
        {
            Config = config;
            FolderPath = destination;
            FilePath = Path.Combine(FolderPath, $"{Config.RootFileName}.md");

            var directInfo = new DirectoryInfo(destination);

            if (!directInfo.Exists)
                Directory.CreateDirectory(destination);

            if (Config.CleanDestination)
                directInfo.Empty();

            foreach (var namespaceGroup in Namespaces)
            {
                var namespaceFolders = namespaceGroup.FullName.Replace('.', Path.DirectorySeparatorChar);
                var namespacePath = Path.Combine(destination, namespaceFolders);
                namespaceGroup.Build(namespacePath, config);
            }

            var content = Config.CurrentTheme.ProjectPart.GetPage(this);

            File.WriteAllText(FilePath, content);
        }
    }
}
