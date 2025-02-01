using System.Diagnostics.CodeAnalysis;
using CsharpToColouredHTML.Core.Emitters.HTML;

namespace Markdown.ColorCode.CSharpToColoredHtml.Internal;

/// <inheritdoc cref="IHtmlFormatterFactory"/>
[ExcludeFromCodeCoverage]
internal sealed class HtmlFormatterFactoryWithCSharpToColoredHtml : IHtmlFormatterFactory
{
    private readonly StyleDictionary _styleDictionary;

    private readonly HTMLEmitterSettings _htmlEmitterSettings;

    /// <summary>
    ///     Create a new <see cref="HtmlFormatterFactoryWithCSharpToColoredHtml"/>.
    /// </summary>
    /// <param name="styleDictionary">The <see cref="StyleDictionary"/> to use with the returned <see cref="IHtmlFormatter"/>s.</param>
    /// <param name="htmlEmitterSettings">The <see cref="HTMLEmitterSettings"/> to use with the returned <see cref="IHtmlFormatter"/>s.</param>
    public HtmlFormatterFactoryWithCSharpToColoredHtml(StyleDictionary styleDictionary, HTMLEmitterSettings htmlEmitterSettings)
    {
        _styleDictionary = styleDictionary;
        _htmlEmitterSettings = htmlEmitterSettings;
    }

    /// <inheritdoc />
    public IHtmlFormatter Get(HtmlFormatterType htmlFormatterType) => htmlFormatterType switch
    {
        HtmlFormatterType.Style => new HtmlStyleFormatter(_styleDictionary),
        HtmlFormatterType.Css => new HtmlCssFormatter(_styleDictionary),
        HtmlFormatterType.StyleWithCSharpToColoredHtml => new CSharpToColoredHtmlFormatter(new HtmlStyleFormatter(_styleDictionary), _htmlEmitterSettings),
        HtmlFormatterType.CssWithCSharpToColoredHtml => new CSharpToColoredHtmlFormatter(new HtmlCssFormatter(_styleDictionary), _htmlEmitterSettings),
        _ => throw new ArgumentOutOfRangeException(nameof(htmlFormatterType), htmlFormatterType, null)
    };
}
