using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System.IO;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    internal class DefaultResolver : IResolver
    {
        private string _rootFileName;
        private string _rootName;

        public DefaultResolver(string rootFileName, string rootName)
        {
            _rootFileName = rootFileName;
            _rootName = rootName;
        }


        public string GetPath(IMarkdownItem item)
        {
            switch (item)
            {
                case MarkdownProject proj:
                    return _rootName;
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

        public string GetFileName(IMarkdownItem item)
        {
            switch (item)
            {
                case MarkdownProject proj:
                    return _rootFileName;
                case MarkdownNamespace nameItem:
                    return _rootFileName;
                case MarkdownProperty prop:
                    return null;
                    return $"{prop.ParentType.Name}-{prop.InternalItem.MetadataToken}.md";
                case MarkdownType type:
                    return $"{Cleaner.CleanName(type.Name, true, false)}.md";
                case MarkdownEnum enumItem:
                    return $"{Cleaner.CleanName(enumItem.Name, true, false)}.md";
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
                    return $"{Cleaner.CleanName(item.FullName, true, false)}.md";
            }
        }
    }
}