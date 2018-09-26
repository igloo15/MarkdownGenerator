using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public class DefaultMethodBuilder
    {
        private DefaultOptions _options;

        public DefaultMethodBuilder(DefaultOptions options)
        {
            _options = options;
        }

        public string BuildPage(MarkdownMethod item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }

    }
}
