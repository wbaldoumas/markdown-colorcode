using System.Diagnostics.CodeAnalysis;

namespace Markdown.ColorCode.Internal;

/// <inheritdoc cref="IHtmlFormatterFactory"/>
[ExcludeFromCodeCoverage]
internal sealed class HtmlFormatterFactory : IHtmlFormatterFactory
{
    private readonly StyleDictionary _styleDictionary;

    /// <summary>
    ///     Create a new <see cref="HtmlFormatterFactory"/>.
    /// </summary>
    /// <param name="styleDictionary">The <see cref="StyleDictionary"/> to use with the returned <see cref="IHtmlFormatter"/>s.</param>
    public HtmlFormatterFactory(StyleDictionary styleDictionary) => _styleDictionary = styleDictionary;

    /// <inheritdoc />
    public IHtmlFormatter Get(HtmlFormatterType htmlFormatterType) => htmlFormatterType switch
    {
        HtmlFormatterType.Style => new HtmlStyleFormatter(_styleDictionary),
        HtmlFormatterType.Css => new HtmlCssFormatter(_styleDictionary),
        HtmlFormatterType.StyleWithCSharpToColoredHtml => throw new NotSupportedException("In order to use StyleWithCSharpToColoredHtml you must install Markdown.ColorCode.CSharpToColoredHtml and invoke UseColorCodeWithCSharpToColoredHtml."),
        HtmlFormatterType.CssWithCSharpToColoredHtml => throw new NotSupportedException("In order to use CssWithCSharpToColoredHtml you must install Markdown.ColorCode.CSharpToColoredHtml and invoke UseColorCodeWithCSharpToColoredHtml."),
        _ => throw new ArgumentOutOfRangeException(nameof(htmlFormatterType), htmlFormatterType, null)
    };
}
