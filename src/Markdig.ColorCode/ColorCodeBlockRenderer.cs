using ColorCode;
using ColorCode.Styling;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using System.Text;

namespace Markdig.ColorCode;

/// <summary>
///     An <see cref="HtmlObjectRenderer{TObject}"/> which colorizes code blocks.
/// </summary>
public class ColorCodeBlockRenderer : HtmlObjectRenderer<CodeBlock>
{
    private readonly CodeBlockRenderer _underlyingRenderer;
    private readonly StyleDictionary _styleDictionary;

    /// <summary>
    ///     Create a new <see cref="ColorCodeBlockRenderer"/> with the specified <paramref name="underlyingRenderer"/> and <paramref name="styleDictionary"/>.
    /// </summary>
    /// <param name="underlyingRenderer">The underlying <see cref="CodeBlockRenderer"/> to handle unsupported languages.</param>
    /// <param name="styleDictionary">A <see cref="StyleDictionary"/> for custom styling.</param>
    public ColorCodeBlockRenderer(CodeBlockRenderer underlyingRenderer, StyleDictionary styleDictionary)
    {
        _underlyingRenderer = underlyingRenderer;
        _styleDictionary = styleDictionary;
    }

    /// <summary>
    /// Writes the specified <see cref="MarkdownObject"/> to the <paramref name="renderer"/>.
    /// </summary>
    /// <param name="renderer">The renderer.</param>
    /// <param name="codeBlock">The code block to render.</param>
    protected override void Write(HtmlRenderer renderer, CodeBlock codeBlock)
    {
        if (codeBlock is not FencedCodeBlock fencedCodeBlock || codeBlock.Parser is not FencedCodeBlockParser parser)
        {
            _underlyingRenderer.Write(renderer, codeBlock);
            return;
        }

        var languageCode = fencedCodeBlock.Info!.Replace(parser.InfoPrefix!, string.Empty);
        var language = Languages.FindById(languageCode);

        if (string.IsNullOrWhiteSpace(languageCode))
        {
            _underlyingRenderer.Write(renderer, codeBlock);

            return;
        }

        var code = ExtractSourceCode(codeBlock);
        var formatter = new HtmlFormatter(_styleDictionary);
        var html = formatter.GetHtmlString(code, language);

        renderer.Write(html);
    }

    private static string ExtractSourceCode(LeafBlock node)
    {
        var code = new StringBuilder();
        var lines = node.Lines.Lines;
        var totalLines = lines.Length;

        for (var index = 0; index < totalLines; index++)
        {
            var line = lines[index];
            var slice = line.Slice;

            if (slice.Text == null)
            {
                continue;
            }

            var lineText = slice.Text.Substring(slice.Start, slice.Length);

            if (index > 0)
            {
                code.AppendLine();
            }

            code.Append(lineText);
        }

        return code.ToString();
    }
}