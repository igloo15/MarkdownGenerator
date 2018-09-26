using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.MarkdownItems;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using Igloo15.MarkdownApi.Core.Themes.Default;

namespace Igloo15.MarkdownApi.Core.Themes
{
    public class DefaultTheme : ITheme
    {
        private DefaultOptions _options;

        private DefaultEnumBuilder _enumBuilder;
        private DefaultNamespaceBuilder _namespaceBuilder;
        private DefaultProjectBuilder _projectBuilder;
        private DefaultTypeBuilder _typeBuilder;

        public DefaultTheme(DefaultOptions options)
        {
            _options = options;
            _enumBuilder = new DefaultEnumBuilder(_options);
            _namespaceBuilder = new DefaultNamespaceBuilder(_options);
            _projectBuilder = new DefaultProjectBuilder(_options);
            _typeBuilder = new DefaultTypeBuilder(_options);
        }

        public string Name => "Default";

        public IResolver Resolver => new DefaultResolver(_options);

        public string BuildPage(MarkdownNamespace item)
        {
            if (!_options.BuildNamespacePages)
                return "";

            return _namespaceBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownProject item)
        {
            return _projectBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownType item)
        {
            if (!_options.BuildTypePages)
                return "";
            return _typeBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownEnum item)
        {
            if (!_options.BuildTypePages)
                return "";
            return _enumBuilder.BuildPage(item);
        }

        public string BuildPage(MarkdownField item)
        {
            return "";
        }

        public string BuildPage(MarkdownProperty item)
        {
            return "";
        }

        public string BuildPage(MarkdownMethod item)
        {
            return "";
        }

        public string BuildPage(MarkdownEvent item)
        {
            return "";
        }

        public string BuildPage(MarkdownConstructor item)
        {
            return "";
        }
    }
}
