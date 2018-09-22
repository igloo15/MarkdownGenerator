using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System.IO;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    internal class DefaultResolver : IResolver
    {
        private string _rootFileName;

        public DefaultResolver(string rootFileName)
        {
            _rootFileName = rootFileName;
        }


        public string GetPath(IMarkdownItem item)
        {
            switch (item)
            {
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
                default:
                    return item.FullName.Replace('.', Path.DirectorySeparatorChar);
            }
        }

        public string GetFileName(IMarkdownItem item)
        {
            switch (item)
            {
                case MarkdownNamespace nameItem:
                    return _rootFileName;
                case MarkdownProperty prop:
                    return null;
                    return $"{prop.ParentType.Name}-{prop.InternalItem.MetadataToken}.md";
                case MarkdownType type:
                    return $"{type.Name}.md";
                case MarkdownEvent eventItem:
                    return null;
                    return $"{eventItem.ParentType.Name}-{eventItem.InternalItem.MetadataToken}.md";
                case MarkdownField field:
                    return null;
                    return $"{field.ParentType.Name}-{field.InternalItem.MetadataToken}.md";
                case MarkdownMethod method:
                    return null;
                    return $"{method.ParentType.Name}-{method.InternalItem.MetadataToken}.md";
                default:
                    return $"{item.FullName}.md";
            }
        }
    }
}