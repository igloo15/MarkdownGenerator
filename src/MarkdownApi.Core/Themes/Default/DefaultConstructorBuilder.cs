using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// The default markdown constructor page builder - Warning this is not yet implemented
    /// </summary>
    public class DefaultConstructorBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// Constructs a constructor page builder - Warning this is not yet implemented
        /// </summary>
        /// <param name="options">The default options for this builder</param>
        public DefaultConstructorBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the page for a constructor item - Warning this is not yet implemented
        /// </summary>
        /// <param name="item">The constructor item</param>
        /// <returns></returns>
        public string BuildPage(MarkdownConstructor item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }
    }
}
