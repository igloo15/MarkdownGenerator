using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    public class MarkdownProject : AbstractMarkdownItem
    {
        public Dictionary<string, IMarkdownItem> AllItems { get; internal set; } = new Dictionary<string, IMarkdownItem>();

        public List<MarkdownNamespace> Namespaces { get; } = new List<MarkdownNamespace>();

        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Project;

        public override string Name { get; }

        public override string FullName { get; }

        public MarkdownProject(string name)
        {
            Name = name;
            FullName = name;
        }

        internal void AddNamespaces(MarkdownNamespace[] namespaces)
        {
            foreach (var namespaceItem in namespaces)
            {
                namespaceItem.InternalProject = this;
                Namespaces.Add(namespaceItem);
            }
        }

        public MarkdownProject Resolve(ITheme theme)
        {
            foreach (var item in AllItems.Values)
            {
                item.As<IInternalMarkdownItem>().SetFilename(theme.Resolver.GetFileName(item));

                item.As<IInternalMarkdownItem>().SetLocation(theme.Resolver.GetPath(item));
            }

            this.Location = theme.Resolver.GetPath(this);
            this.FileName = theme.Resolver.GetFileName(this);

            return this;
        }

        public MarkdownProject Create(ITheme theme)
        {

            var projectContent = theme.BuildPage(this);

            if (!String.IsNullOrEmpty(projectContent))
                File.WriteAllText(Path.Combine(Location, FileName), projectContent);


            foreach(var item in AllItems.Values)
            {
                var content = item.BuildPage(theme);

                if(!String.IsNullOrEmpty(content))
                {
                    var place = Path.Combine(Location, item.Location);
                    var filePath = Path.Combine(place, item.FileName);

                    if (!Directory.Exists(place))
                        Directory.CreateDirectory(place);

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

        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        public override MarkdownProject Project => this;

        public override TypeWrapper TypeInfo => new TypeWrapper(FullName);

        public bool TryGetValue(TypeWrapper wrapper, out IMarkdownItem value)
        {
            return AllItems.TryGetValue(wrapper.GetId(), out value);  
        }
    }
}
