using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultConstructorBuilder
    {
        private DefaultOptions _options;

        public DefaultConstructorBuilder(DefaultOptions options)
        {
            _options = options;
        }

        public string BuildPage(MarkdownConstructor item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }
    }
}
