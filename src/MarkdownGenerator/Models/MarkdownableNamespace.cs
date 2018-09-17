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

        public List<MarkdownableType> Types { get; private set; }

        public string Name => FullName;

        public bool IsStatic => false;

        private Options _config;

        public MarkdownableNamespace(List<MarkdownableType> types, string fullName)
        {
            FullName = fullName;
            Types = types;

            FolderPath = FullName.Replace('.', Path.DirectorySeparatorChar);
        }

        public void Build(string dest, Options config, MarkdownBuilder homeBuilder)
        {
            
        }

        public string GetLink()
        {
            return $"[{FullName}]({FolderPath}{Path.DirectorySeparatorChar}{_config.RootFileName}.md)";
        }

        public string GetName()
        {
            return FullName;
        }

        public string GetReturnOrType()
        {
            return "";
        }

        public string GetSummary()
        {
            return "";
        }

        public string GetCode()
        {
            return $"namespace {FullName}";
        }

        public string GetDetailed()
        {
            return GetCode();
        }

        public string GetExample()
        {
            return GetCode();
        }

        public string BuildPage()
        {
            var namespaceBuilder = new MarkdownBuilder();
            namespaceBuilder.Header(1, FullName);
            namespaceBuilder.AppendLine();
            
            foreach (var item in Types.OrderBy(x => x.Name))
            {
                var sb = new StringBuilder();
                if(_config.TypePages)
                {
                    namespaceBuilder.List(item.GetLink());
                }
                else
                {
                    namespaceBuilder.List(item.GetName());
                }
            }

            namespaceBuilder.AppendLine();
            

            return namespaceBuilder.ToString();
        }

        public void Build(string destination, Options config)
        {
            _config = config;
            var namespaceDirectoryPath = Path.Combine(destination, FolderPath);
            if (!Directory.Exists(namespaceDirectoryPath)) Directory.CreateDirectory(namespaceDirectoryPath);

            if (_config.NamespacePages)
            {
                var content = BuildPage();
                File.WriteAllText(Path.Combine(namespaceDirectoryPath, $"{_config.RootFileName}.md"), content);
            }

            foreach(var item in Types.OrderBy(x => x.Name))
            {
                item.Build(namespaceDirectoryPath, _config);
            }
        }
    }
}
