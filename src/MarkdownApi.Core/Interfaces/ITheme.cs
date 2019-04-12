using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using Microsoft.Extensions.Logging;

namespace igloo15.MarkdownApi.Core.Interfaces
{
    /// <summary>
    /// A theme is used to generate the content for markdown pages as well as where markdown pages should be placed
    /// </summary>
    public interface ITheme
    {
        /// <summary>
        /// The name of the Theme
        /// </summary>
        string Name { get; }

        /// <summary>
        /// Resolves filepaths and filenames for all Markdown Items
        /// </summary>
        IResolver Resolver { get; }

        /// <summary>
        /// Set the default logger to be used during Theme Construction
        /// </summary>
        /// <param name="logger">The Logger</param>
        void SetLogger(ILogger logger);

        /// <summary>
        /// Builds Namespace Pages with the given MarkdownNamespace Item
        /// </summary>
        /// <param name="item">The MarkdownNamespace item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownNamespace item);
        
        /// <summary>
        /// Builds Project Pages with the given MarkdownProject Item
        /// </summary>
        /// <param name="item">The MarkdownProject item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownProject item);
        
        /// <summary>
        /// Builds Type Pages with the given MarkdownType Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownType item);
        
        /// <summary>
        /// Builds Enum Pages with the given MarkdownEnum Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownEnum item);
        
        /// <summary>
        /// Builds Field Pages with the given MarkdownField Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownField item);
        
        /// <summary>
        /// Builds Property Pages with the given MarkdownProperty Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownProperty item);
        
        /// <summary>
        /// Builds Method Pages with the given MarkdownMethod Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownMethod item);
        
        /// <summary>
        /// Builds Event Pages with the given MarkdownEvent Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownEvent item);

        /// <summary>
        /// Builds Constructor Pages with the given MarkdownConstructor Item
        /// </summary>
        /// <param name="item">The item to build the page with</param>
        /// <returns>A string of the markdown content or "" if no page created</returns>
        string BuildPage(MarkdownConstructor item);
    }
}
