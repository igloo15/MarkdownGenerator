using Igloo15.MarkdownApi.Core.Interfaces;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Igloo15.MarkdownApi.Core.MarkdownItems
{
    /// <summary>
    /// 
    /// </summary>
    public class MarkdownProject : AbstractMarkdownItem
    {
        /// <summary>
        /// 
        /// </summary>
        public Dictionary<string, IMarkdownItem> AllItems { get; internal set; } = new Dictionary<string, IMarkdownItem>();

        /// <summary>
        /// The type of markdown item
        /// </summary>
        public override MarkdownItemTypes ItemType => MarkdownItemTypes.Project;

        /// <summary>
        /// The Name of the Markdown item
        /// </summary>
        public override string Name { get; }

        /// <summary>
        /// The full name of the Markdown Item
        /// </summary>
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

        /// <summary>
        /// Build the Markdown Project and all items in the project with the given field in the given location. This method is shorthand for calling Resolve and then Create
        /// </summary>
        /// <param name="theme">The theme to use to create the markdown</param>
        /// <param name="outputLocation">The location to put all the markdown files</param>
        public void Build(ITheme theme, string outputLocation)
        {
            Resolve(theme);

            Create(theme, outputLocation);
        }

        /// <summary>
        /// Create a page for this markdown item or "" if no page is created
        /// </summary>
        /// <param name="theme">The theme to be used to in building the page</param>
        /// <returns>The text content of the page or "" if no page content</returns>
        public override string BuildPage(ITheme theme)
        {
            return theme.BuildPage(this);
        }

        /// <summary>
        /// The markdown project the item is part of
        /// </summary>
        public override MarkdownProject Project => this;

        /// <summary>
        /// The type info of the MarkdownItem used to find references to it from other MarkdownItems
        /// </summary>
        public override TypeWrapper TypeInfo => new TypeWrapper(FullName);

        /// <summary>
        /// Try to get a markdownitem with the given TypeWrapper lookup key
        /// </summary>
        /// <param name="wrapper">The key to look up the markdownitem</param>
        /// <param name="value">If found the IMarkdownItem</param>
        /// <returns>True if found or false if not found</returns>
        public bool TryGetValue(TypeWrapper wrapper, out IMarkdownItem value)
        {
            return AllItems.TryGetValue(wrapper.GetId(), out value);  
        }
    }
}
