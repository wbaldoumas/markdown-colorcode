namespace Markdown.ColorCode.Internal;

/// <summary>
///     A Markdig extension which colorizes code using ColorCode.
/// </summary>
internal sealed class ColorCodeExtension : IMarkdownExtension
{
    private readonly ILanguageExtractor _languageExtractor;

    private readonly ICodeExtractor _codeExtractor;

    private readonly IHtmlFormatter _htmlFormatter;

    /// <summary>
    ///     Create a new <see cref="ColorCodeExtension"/>.
    /// </summary>
    /// <param name="languageExtractor">The <see cref="ILanguageExtractor"/> to use with the extension.</param>
    /// <param name="codeExtractor">The <see cref="ICodeExtractor"/> to use with the extension.</param>
    /// <param name="htmlFormatter">The <see cref="IHtmlFormatter"/> to use with the extension.</param>
    public ColorCodeExtension(
        ILanguageExtractor languageExtractor,
        ICodeExtractor codeExtractor,
        IHtmlFormatter htmlFormatter)
    {
        _languageExtractor = languageExtractor;
        _codeExtractor = codeExtractor;
        _htmlFormatter = htmlFormatter;
    }

    /// <summary>
    ///     Sets up this extension for the specified pipeline.
    /// </summary>
    /// <param name="pipeline">The pipeline.</param>
    public void Setup(MarkdownPipelineBuilder pipeline)
    {
    }

    /// <summary>
    ///     Sets up this extension for the specified renderer.
    /// </summary>
    /// <param name="pipeline">The pipeline used to parse the document.</param>
    /// <param name="renderer">The renderer.</param>
    public void Setup(MarkdownPipeline pipeline, IMarkdownRenderer renderer)
    {
        if (renderer is not TextRendererBase<HtmlRenderer> htmlRenderer)
        {
            return;
        }

        var codeBlockRenderer = htmlRenderer.ObjectRenderers.FindExact<CodeBlockRenderer>();

        if (codeBlockRenderer is not null)
        {
            htmlRenderer.ObjectRenderers.Remove(codeBlockRenderer);
        }
        else
        {
            codeBlockRenderer = new CodeBlockRenderer();
        }

        htmlRenderer.ObjectRenderers.AddIfNotAlready(
            new ColorCodeBlockRenderer(
                codeBlockRenderer,
                _languageExtractor,
                _codeExtractor,
                _htmlFormatter
            )
        );
    }
}
