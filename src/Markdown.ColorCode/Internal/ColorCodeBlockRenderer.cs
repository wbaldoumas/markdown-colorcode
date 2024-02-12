namespace Markdown.ColorCode.Internal;

/// <summary>
///     A renderer which colorizes code blocks using ColorCode.
/// </summary>
internal sealed class ColorCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _underlyingCodeBlockRenderer;
    private readonly ILanguageExtractor _languageExtractor;
    private readonly ICodeExtractor _codeExtractor;
    private readonly IHtmlFormatter _htmlFormatter;

    /// <summary>
    ///     Create a new <see cref="ColorCodeBlockRenderer"/>.
    /// </summary>
    /// <param name="underlyingCodeBlockRenderer">The underlying CodeBlockRenderer to handle unsupported languages.</param>
    /// <param name="languageExtractor"> A <see cref="ILanguageExtractor"/> used to extract the <see cref="ILanguage"/> from the <see cref="CodeBlock"/>.</param>
    /// <param name="codeExtractor">A <see cref="ICodeExtractor"/> used to extract the code from the <see cref="CodeBlock"/>.</param>
    /// <param name="htmlFormatter">A <see cref="IHtmlFormatter"/> for generating HTML strings.</param>
    public ColorCodeBlockRenderer(
        CodeBlockRenderer underlyingCodeBlockRenderer,
        ILanguageExtractor languageExtractor,
        ICodeExtractor codeExtractor,
        IHtmlFormatter htmlFormatter)
    {
        _underlyingCodeBlockRenderer = underlyingCodeBlockRenderer;
        _languageExtractor = languageExtractor;
        _codeExtractor = codeExtractor;
        _htmlFormatter = htmlFormatter;
    }

    /// <summary>
    ///     Writes the specified <paramref name="codeBlock"/> to the <paramref name="renderer"/>.
    /// </summary>
    /// <param name="renderer">The renderer.</param>
    /// <param name="codeBlock">The code block to render.</param>
    protected override void Write(HtmlRenderer renderer, CodeBlock codeBlock)
    {
        if (codeBlock is not FencedCodeBlock fencedCodeBlock ||
            codeBlock.Parser is not FencedCodeBlockParser fencedCodeBlockParser)
        {
            _underlyingCodeBlockRenderer.Write(renderer, codeBlock);

            return;
        }

        var language = _languageExtractor.ExtractLanguage(fencedCodeBlock, fencedCodeBlockParser);

        if (language is null)
        {
            _underlyingCodeBlockRenderer.Write(renderer, codeBlock);

            return;
        }

        var code = _codeExtractor.ExtractCode(codeBlock);
        var html = _htmlFormatter.GetHtmlString(code, language);

        renderer.Write(html);
    }
}
