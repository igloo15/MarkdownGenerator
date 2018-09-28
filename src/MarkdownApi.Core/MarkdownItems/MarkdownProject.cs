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

        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Project;

        public override string Name { get; }

        public override string FullName { get; }

        public MarkdownProject()
        {
            Name = null;
            FullName = null;
        }
        
        public MarkdownProject Resolve(ITheme theme)
        {
            foreach (var item in AllItems.Values)
            {
                if (item.ItemType == MarkdownItemTypes.Namespace)
                    item.As<MarkdownNamespace>().InternalProject = this;

                item.As<IInternalMarkdownItem>().SetFilename(theme.Resolver.GetFileName(item));

                item.As<IInternalMarkdownItem>().SetLocation(theme.Resolver.GetPath(item));
            }

            this.Location = theme.Resolver.GetPath(this);
            this.FileName = theme.Resolver.GetFileName(this);

            return this;
        }

        public MarkdownProject Create(ITheme theme, string outputLocation)
        {
            var rootLocation = Path.Combine(outputLocation, Location);
            var projectContent = theme.BuildPage(this);

            if (!Directory.Exists(rootLocation))
                Directory.CreateDirectory(rootLocation);

            if (!String.IsNullOrEmpty(projectContent))
                File.WriteAllText(Path.Combine(rootLocation, FileName), projectContent);
            
            foreach (var item in AllItems.Values)
            {
                var content = item.BuildPage(theme);

                if(!String.IsNullOrEmpty(content))
                {
                    var place = Path.Combine(rootLocation, item.Location);
                    var filePath = Path.Combine(place, item.FileName);

                    if (!Directory.Exists(place))
                        Directory.CreateDirectory(place);

                    File.WriteAllText(filePath, content);
                }
            }

            return this;
        }

        public void Build(ITheme theme, string outputLocation)
        {
            Resolve(theme);

            Create(theme, outputLocation);
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
