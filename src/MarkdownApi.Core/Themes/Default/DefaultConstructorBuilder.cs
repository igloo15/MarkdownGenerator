using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// 
    /// </summary>
    public class DefaultConstructorBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="options"></param>
        public DefaultConstructorBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        public string BuildPage(MarkdownConstructor item)
        {
            MarkdownBuilder mb = new MarkdownBuilder();

            return mb.ToString();
        }
    }
}
