using Igloo15.MarkdownApi.Core.Interfaces;
using Microsoft.Extensions.Logging;
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

        internal MarkdownProject()
        {
            Name = null;
            FullName = null;
        }
        
        /// <summary>
        /// Resolve the file names and locations based on the ITheme provided
        /// </summary>
        /// <param name="theme"></param>
        /// <returns></returns>
        public MarkdownProject Resolve(ITheme theme)
        {
            Constants.Logger?.LogInformation("Resolving File Paths and File Names");
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

        /// <summary>
        /// Create the Markdown Api Pages based on the ITheme provided and put all files in the given location
        /// </summary>
        /// <param name="theme"></param>
        /// <param name="outputLocation"></param>
        /// <returns></returns>
        public MarkdownProject Create(ITheme theme, string outputLocation)
        {
            Constants.Logger?.LogInformation("Create Markdown Page Content and Files");
            var rootLocation = Path.Combine(outputLocation, Location);
            var projectContent = theme.BuildPage(this);

            if (!Directory.Exists(rootLocation))
                Directory.CreateDirectory(rootLocation);

            if (!String.IsNullOrEmpty(projectContent))
                File.WriteAllText(Path.Combine(rootLocation, FileName), projectContent);
            int pageCount = 0;
            foreach (var item in AllItems.Values)
            {
                var content = item.BuildPage(theme);

                if(!String.IsNullOrEmpty(content))
                {
                    pageCount++; 
                    var place = Path.Combine(rootLocation, item.Location);
                    var filePath = Path.Combine(place, item.FileName);

                    if (!Directory.Exists(place))
                        Directory.CreateDirectory(place);

                    File.WriteAllText(filePath, content);
                }
            }
            Constants.Logger?.LogInformation("All Content Created");
            Constants.Logger?.LogInformation("{pageCount} Markdown Pages created at {output}", pageCount, outputLocation);
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
