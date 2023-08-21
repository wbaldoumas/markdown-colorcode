namespace Markdown.ColorCode;

/// <summary>
///     Extensions for adding ColorCode code colorization to the Markdig <see cref="MarkdownPipelineBuilder"/>.
/// </summary>
public static class MarkdownPipelineBuilderExtensions
{
    /// <summary>
    ///     Use ColorCode to colorize HTML generated from Markdown.
    /// </summary>
    /// <param name="markdownPipelineBuilder">The <see cref="MarkdownPipelineBuilder"/> to configure.</param>
    /// <param name="htmlFormatterType">Optional. The type of HTML formatter to use when generating HTML from Markdown.</param>
    /// <param name="styleDictionary">Optional. The styles to use when generating HTML from Markdown.</param>
    /// <param name="additionalLanguages">Optional. Additional languages used to augment the built-in languages provided by ColorCode-Universal.</param>
    /// <param name="defaultLanguageId">Optional. The default language to use if a given language can't be found.</param>
    /// <returns>The <see cref="MarkdownPipelineBuilder"/> configured with ColorCode.</returns>
    public static MarkdownPipelineBuilder UseColorCode(
        this MarkdownPipelineBuilder markdownPipelineBuilder,
        HtmlFormatterType htmlFormatterType = HtmlFormatterType.Style,
        StyleDictionary? styleDictionary = null,
        IEnumerable<ILanguage>? additionalLanguages = null,
        string? defaultLanguageId = null)
    {
        var languageExtractor = new LanguageExtractor(
            additionalLanguages ?? Enumerable.Empty<ILanguage>(),
            defaultLanguageId ?? string.Empty
        );

        var codeExtractor = new CodeExtractor();
        var htmlFormatterFactory = new HtmlFormatterFactory(styleDictionary ?? StyleDictionary.DefaultDark);
        var htmlFormatter = htmlFormatterFactory.Get(htmlFormatterType);
        var colorCodeExtension = new ColorCodeExtension(languageExtractor, codeExtractor, htmlFormatter);

        markdownPipelineBuilder.Extensions.Add(colorCodeExtension);

        return markdownPipelineBuilder;
    }
}
