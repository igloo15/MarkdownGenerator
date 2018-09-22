using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    public class MarkdownProject
    {
        public string Root { get; private set; }

        public Dictionary<string, IMarkdownItem> AllItems { get; internal set; } = new Dictionary<string, IMarkdownItem>();

        public List<MarkdownNamespace> Namespaces { get; } = new List<MarkdownNamespace>();

        public MarkdownProject(string root)
        {
            Root = root;
        }

        internal void AddNamespaces(MarkdownNamespace[] namespaces)
        {
            foreach (var namespaceItem in namespaces)
            {
                namespaceItem.Project = this;
                Namespaces.Add(namespaceItem);
            }
        }

        public MarkdownProject Resolve(ITheme theme)
        {
            foreach (var item in AllItems.Values)
            {
                item.SetFileName(theme.Resolver.GetFileName(item));

                item.SetLocation(theme.Resolver.GetPath(item));
            }

            return this;
        }

        public MarkdownProject Create(ITheme theme)
        {

            var projectContent = theme.BuildPage(this);

            if (!String.IsNullOrEmpty(projectContent))
                File.WriteAllText(Path.Combine(Root, theme.RootName), projectContent);


            foreach(var item in AllItems.Values)
            {
                var content = item.BuildPage(theme);

                if(!String.IsNullOrEmpty(content))
                {
                    var place = Path.Combine(Root, item.Location);
                    var filePath = Path.Combine(place, item.FileName);

                    if (!Directory.Exists(place))
                        Directory.CreateDirectory(Path.Combine(Root, item.Location));

                    File.WriteAllText(filePath, content);
                }
            }

            return this;
        }

        public void Build(ITheme theme)
        {
            Resolve(theme);

            Create(theme);
        }
    }
}
