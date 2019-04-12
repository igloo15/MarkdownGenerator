using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// Default markdown method page builder - Warning this is not yet implemented
    /// </summary>
    public class DefaultMethodBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// Constructs a markdown method page builder  - Warning this is not yet implemented
        /// </summary>
        /// <param name="options">The default options to be constructed with</param>
        public DefaultMethodBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the markdown method page content - Warning this is not yet implemented
        /// </summary>
        /// <param name="item">The markdown method item</param>
        /// <returns>The markdown content</returns>
        public string BuildPage(MarkdownMethod item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }

    }
}
