namespace Markdown.ColorCode.Internal;

/// <summary>
///     A factory for getting <see cref="IHtmlFormatter"/>s.
/// </summary>
internal interface IHtmlFormatterFactory
{
    /// <summary>
    ///     Get a <see cref="IHtmlFormatter"/> for the given <paramref name="htmlFormatterType"/>.
    /// </summary>
    /// <param name="htmlFormatterType">The <see cref="HtmlFormatterType"/> to get the <see cref="IHtmlFormatter"/> for.</param>
    /// <returns>The <see cref="IHtmlFormatter"/></returns>
    IHtmlFormatter Get(HtmlFormatterType htmlFormatterType);
}
