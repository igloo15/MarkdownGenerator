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

        public MarkdownableNamespace[] Namespaces { get; private set; }

        private Options _config;

        public MarkdownableProject(Options config, MarkdownableNamespace[] namespaces)
        {
            Name = config.RootTitle;
            FolderPath = config.Destination;
            Namespaces = namespaces;
            _config = config;
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

        public string BuildPage()
        {
            var destination = new DirectoryInfo(FolderPath);
            if (!destination.Exists)
                Directory.CreateDirectory(FolderPath);

            if (_config.CleanDestination)
                destination.Empty();

            // Home Markdown Builder
            var homeBuilder = new MarkdownBuilder();
            homeBuilder.Header(1, _config.RootTitle);
            homeBuilder.AppendLine();

            foreach (var g in Namespaces)
            {
                if (_config.NamespacePages)
                {
                    homeBuilder.Header(2, GetLink());
                }
                else
                {
                    homeBuilder.Header(2, GetName());
                }

                homeBuilder.AppendLine();

                foreach (var item in g.Types.OrderBy(x => x.Name))
                {
                    var sb = new StringBuilder();
                    

                    if (_config.TypePages)
                    {
                        homeBuilder.List(item.GetLink());
                    }
                    else
                    {
                        homeBuilder.List(item.GetName());
                    }
                }
            }

            homeBuilder.AppendLine();

            return homeBuilder.ToString();
        }

        public void Build(string destination)
        {
            foreach(var namespaceGroup in Namespaces)
            {
                namespaceGroup.Build(destination);
            }

            var content = BuildPage();

            File.WriteAllText(Path.Combine(destination, $"{_config.RootFileName}.md"), content);
        }
    }
}
