using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    internal class NamespaceGroup
    {
        public string FullName { get; private set; }

        public string FolderPath { get; private set; }

        public List<MarkdownableType> Types { get; private set; }

        public NamespaceGroup(List<MarkdownableType> types, string fullName)
        {
            FullName = fullName;
            Types = types;

            FolderPath = FullName.Replace('.', Path.DirectorySeparatorChar);
        }

        public void Build(string dest, Options config, MarkdownBuilder homeBuilder)
        {
            var namespaceDirectoryPath = Path.Combine(dest, FolderPath);
            if (!Directory.Exists(namespaceDirectoryPath)) Directory.CreateDirectory(namespaceDirectoryPath);

            var namespaceBuilder = new MarkdownBuilder();
            namespaceBuilder.Header(1, FullName);
            namespaceBuilder.AppendLine();

            if (!config.NamespacePages)
                homeBuilder.HeaderWithLink(2, FullName, FullName);
            else
                homeBuilder.HeaderWithLink(2, FullName, Path.Combine(FolderPath, config.RootFileName + ".md"));

            homeBuilder.AppendLine();

            foreach (var item in Types.OrderBy(x => x.Name))
            {
                var sb = new StringBuilder();
                homeBuilder.ListLink(MarkdownBuilder.MarkdownCodeQuote(item.BeautifyName), Path.Combine(FolderPath, item.Name + ".md"));
                namespaceBuilder.ListLink(MarkdownBuilder.MarkdownCodeQuote(item.BeautifyName), Path.Combine(FolderPath, item.Name + ".md"));

                sb.Append(item.ToString());
                File.WriteAllText(Path.Combine(namespaceDirectoryPath, item.Name + ".md"), sb.ToString());

                if (config.MethodPages)
                    item.GenerateMethodDocuments(namespaceDirectoryPath);
            }

            homeBuilder.AppendLine();
            namespaceBuilder.AppendLine();
            if (config.NamespacePages)
                File.WriteAllText(Path.Combine(namespaceDirectoryPath, $"{config.RootFileName}.md"), namespaceBuilder.ToString());
        }
    }
}
