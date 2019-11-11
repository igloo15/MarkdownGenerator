using igloo15.MarkdownApi.Core.Interfaces;
using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using igloo15.MarkdownApi.Core.Themes.Default;
using Microsoft.Extensions.Logging;

namespace igloo15.MarkdownApi.Core.Themes
{
    /// <summary>
    /// This default theme is bundled with the core to provide an example and base theming for documentation generated
    /// </summary>
    public class DefaultTheme : ITheme
  {
    private DefaultOptions _options;

    private DefaultEnumBuilder _enumBuilder;
    private DefaultNamespaceBuilder _namespaceBuilder;
    private DefaultProjectBuilder _projectBuilder;
    private DefaultTypeBuilder _typeBuilder;
    private DefaultMethodBuilder _methodBuilder;
    internal static ILogger ThemeLogger;

    /// <summary>
    /// Constructs a default theme with the given options
    /// </summary>
    /// <param name="options">The options to configure the default theme</param>
    public DefaultTheme(DefaultOptions options)
    {
      _options = options.LoadFile();
      _enumBuilder = new DefaultEnumBuilder(_options);
      _namespaceBuilder = new DefaultNamespaceBuilder(_options);
      _projectBuilder = new DefaultProjectBuilder(_options);
      _typeBuilder = new DefaultTypeBuilder(_options);
      _methodBuilder = new DefaultMethodBuilder(_options);
    }

    /// <summary>
    /// The name of this theme "Default"
    /// </summary>
    public string Name => "Default";

    /// <summary>
    /// The Default Resolver for this theme
    /// </summary>
    public IResolver Resolver => new DefaultResolver(_options);

    /// <summary>
    /// Set the default logger to be used during Theme Construction
    /// </summary>
    /// <param name="logger">The Logger</param>
    public void SetLogger(ILogger logger)
    {
      ThemeLogger = logger;
    }

    /// <summary>
    /// Builds Namespace Pages with the given MarkdownNamespace Item
    /// </summary>
    /// <param name="item">The MarkdownNamespace item to build the page with</param>
    /// <returns>A string of the markdown content or "" if no page created</returns>
    public string BuildPage(MarkdownNamespace item)
    {
      if (!_options.BuildNamespacePages)
        return "";

      return _namespaceBuilder.BuildPage(item);
    }

    /// <summary>
    /// Builds Project Pages with the given MarkdownProject Item
    /// </summary>
    /// <param name="item">The MarkdownProject item to build the page with</param>
    /// <returns>A string of the markdown content or "" if no page created</returns>
    public string BuildPage(MarkdownProject item)
    {
      return _projectBuilder.BuildPage(item);
    }

    /// <summary>
    /// Builds Type Pages with the given MarkdownType Item
    /// </summary>
    /// <param name="item">The item to build the page with</param>
    /// <returns>A string of the markdown content or "" if no page created</returns>
    public string BuildPage(MarkdownType item)
    {
      if (!_options.BuildTypePages)
        return "";
      return _typeBuilder.BuildPage(item);
    }

    /// <summary>
    /// Builds Enum Pages with the given MarkdownEnum Item
    /// </summary>
    /// <param name="item">The item to build the page with</param>
    /// <returns>A string of the markdown content or "" if no page created</returns>
    public string BuildPage(MarkdownEnum item)
    {
      if (!_options.BuildTypePages)
        return "";
      return _enumBuilder.BuildPage(item);
    }

    /// <summary>
    /// In default theme this returns only ""
    /// </summary>
    /// <param name="item">The item to build a page with</param>
    /// <returns>An empty string</returns>
    public string BuildPage(MarkdownField item)
    {
      return "";
    }

    /// <summary>
    /// In default theme this returns only ""
    /// </summary>
    /// <param name="item">The item to build a page with</param>
    /// <returns>An empty string</returns>
    public string BuildPage(MarkdownProperty item)
    {
      return "";
    }

    /// <summary>
    /// In default theme this returns only ""
    /// </summary>
    /// <param name="item">The item to build a page with</param>
    /// <returns>An empty string</returns>
    public string BuildPage(MarkdownMethod item)
    {
      if (!_options.BuildMethodPages)
        return "";

      return _methodBuilder.BuildPage(item);
    }

    /// <summary>
    /// In default theme this returns only ""
    /// </summary>
    /// <param name="item">The item to build a page with</param>
    /// <returns>An empty string</returns>
    public string BuildPage(MarkdownEvent item)
    {
      return "";
    }

    /// <summary>
    /// In default theme this returns only ""
    /// </summary>
    /// <param name="item">The item to build a page with</param>
    /// <returns>An empty string</returns>
    public string BuildPage(MarkdownConstructor item)
    {
      return "";
    }



  }
}
