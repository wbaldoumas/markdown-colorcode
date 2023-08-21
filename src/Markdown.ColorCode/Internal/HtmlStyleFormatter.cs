namespace Markdown.ColorCode.Internal;

/// <inheritdoc cref="IHtmlFormatter"/>
internal sealed class HtmlStyleFormatter : IHtmlFormatter
{
    private readonly HtmlFormatter _internalFormatter;

    /// <summary>
    ///     Create a new <see cref="HtmlStyleFormatter"/>
    /// </summary>
    /// <param name="styleDictionary">The style dictionary to apply when generating HTML.</param>
    public HtmlStyleFormatter(StyleDictionary styleDictionary) =>
        _internalFormatter = new HtmlFormatter(styleDictionary);

    /// <inheritdoc />
    public string? GetHtmlString(string sourceCode, ILanguage language) =>
        _internalFormatter.GetHtmlString(sourceCode, language);
}
