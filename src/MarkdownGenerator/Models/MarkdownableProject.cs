using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator.Models
{
    internal class MarkdownableProject : IMarkdownable
    {
        public string FolderPath { get; private set; }

        public string Name { get; private set; }

        public bool IsStatic => false;

        public Options Config { get; private set; }

        public MarkdownableNamespace[] Namespaces { get; private set; }

        public MarkdownableProject(MarkdownableNamespace[] namespaces, Options config)
        {
            Config = config;
            Name = config.RootTitle;
            FolderPath = config.Destination;
            Namespaces = namespaces;
            Config = config;
        }

        public string GetCode()
        {
            return "";
        }

        public string GetDetailed()
        {
            return "";
        }

        public string GetExample()
        {
            return "";
        }

        public string GetLink()
        {
            return $"[{Name}]({Path.Combine(FolderPath, $"{Config.RootFileName}.md")}";
        }

        public string GetName()
        {
            return Name;
        }

        public string GetReturnOrType()
        {
            return "";
        }

        public string GetSummary()
        {
            return "";
        }
        
        public void Build(string destination, Options config)
        {
            Config = config;

            var directInfo = new DirectoryInfo(destination);

            if (!directInfo.Exists)
                Directory.CreateDirectory(destination);

            if (Config.CleanDestination)
                directInfo.Empty();

            foreach (var namespaceGroup in Namespaces)
            {
                namespaceGroup.Build(destination, config);
            }

            var content = Config.CurrentTheme.ProjectPart.GetPage(this);

            File.WriteAllText(Path.Combine(destination, $"{Config.RootFileName}.md"), content);
        }
    }
}
