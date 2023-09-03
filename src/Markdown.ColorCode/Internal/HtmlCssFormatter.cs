namespace Markdown.ColorCode.Internal;

/// <inheritdoc cref="IHtmlFormatter"/>
internal sealed class HtmlCssFormatter : IHtmlFormatter
{
    private readonly HtmlClassFormatter _internalFormatter;

    /// <summary>
    ///     Create a new <see cref="HtmlStyleFormatter"/>
    /// </summary>
    /// <param name="styleDictionary">The style dictionary to apply when generating HTML.</param>
    public HtmlCssFormatter(StyleDictionary styleDictionary) =>
        _internalFormatter = new HtmlClassFormatter(styleDictionary);

    /// <inheritdoc />
    public string? GetHtmlString(string sourceCode, ILanguage language) =>
        _internalFormatter.GetHtmlString(sourceCode, language);
}
