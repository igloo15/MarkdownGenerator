using igloo15.MarkdownApi.Core.Interfaces;
using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using System.IO;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// This default resolver is used to resolve the location to each page
    /// </summary>
    public class DefaultResolver : IResolver
    {
        private DefaultOptions _options;

        /// <summary>
        /// Constructs the Default Resolver using the given options
        /// </summary>
        /// <param name="options">The options for the resolver</param>
        public DefaultResolver(DefaultOptions options)
        {
            _options = options;
        }


        /// <summary>
        /// Calculates the path for the markdown item
        /// </summary>
        /// <param name="item">The item to get a path for</param>
        /// <returns>String path to where to place the markdown item page</returns>
        public string GetPath(IMarkdownItem item)
        {
            switch (item)
            {
                case MarkdownProject proj:
                    return "";
                case MarkdownNamespace nameItem:
                    return nameItem.FullName.Replace('.', Path.DirectorySeparatorChar);
                case MarkdownProperty prop:
                    return Path.Combine(GetPath(prop.ParentType), "Properties");
                case MarkdownType type:
                    return GetPath(type.NamespaceItem);
                case MarkdownEvent eventItem:
                    return Path.Combine(GetPath(eventItem.ParentType), "Events");
                case MarkdownField field:
                    return Path.Combine(GetPath(field.ParentType), "Fields");
                case MarkdownMethod method:
                    return Path.Combine(GetPath(method.ParentType), "Methods");
                case MarkdownEnum enumItem:
                    return GetPath(enumItem.NamespaceItem);
                default:
                    return item.FullName.Replace('.', Path.DirectorySeparatorChar);
            }
        }

        /// <summary>
        /// Gets the filename for the markdown item
        /// </summary>
        /// <param name="item">The item to get a filename for</param>
        /// <returns>String name of the file that the item's content should be in</returns>
        public string GetFileName(IMarkdownItem item)
        {
            switch (item)
            {
                case MarkdownProject proj:
                    return _options.RootFileName;
                case MarkdownNamespace nameItem:
                    if(_options.BuildNamespacePages)
                        return _options.RootFileName;
                    return null;
                case MarkdownType type:
                    if(_options.BuildTypePages)
                        return $"{Cleaner.CleanName(type.Name, true, false)}.md";
                    return null;
                case MarkdownEnum enumItem:
                    if(_options.BuildTypePages)
                        return $"{Cleaner.CleanName(enumItem.Name, true, false)}.md";
                    return null;
                case MarkdownConstructor constructor:
                    if(_options.BuildConstructorPages)
                        return $"{constructor.ParentType.Name}--{constructor.InternalItem.MetadataToken}.md";
                    return null;
                case MarkdownMethod method:
                    if (_options.BuildMethodPages)
                        return $"{method.ParentType.Name}--{method.InternalItem.Name}.md";
                    return null;
            }

            return null;
        }
    }
}
