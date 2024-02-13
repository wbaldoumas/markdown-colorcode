namespace Markdown.ColorCode;

/// <summary>
///     The type of HTML formatter to use when converting markdown code blocks to HTML.
/// </summary>
public enum HtmlFormatterType
{
    /// <summary>
    ///    Use the ColorCode <see cref="HtmlFormatter"/> to format the code using inline style attributes.
    /// </summary>
    Style,

    /// <summary>
    ///     Use the ColorCode <see cref="HtmlClassFormatter"/> to format the code using CSS classes.
    /// </summary>
    Css,

    /// <summary>
    ///    Use the ColorCode <see cref="HtmlFormatter"/> to format the code using inline style attributes, boosted with CsharpToColouredHTML.Core. Only usable with Markdown.ColorCode.CSharpToColoredHtml.
    /// </summary>
    StyleWithCSharpToColoredHtml,

    /// <summary>
    ///    Use the ColorCode <see cref="HtmlClassFormatter"/> to format the code using CSS classes, boosted with CsharpToColouredHTML.Core. Only usable with Markdown.ColorCode.CSharpToColoredHtml.
    /// </summary>
    CssWithCSharpToColoredHtml
}
