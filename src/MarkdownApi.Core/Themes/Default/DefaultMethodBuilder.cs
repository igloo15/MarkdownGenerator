using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultMethodBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DefaultMethodBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string BuildPage(MarkdownMethod item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }

    }
}
