namespace Markdown.ColorCode.Internal;

/// <summary>
///     Formats source code as HTML.
/// </summary>
internal interface IHtmlFormatter
{
    /// <summary>
    ///     Gets the HTML string for the given <paramref name="sourceCode"/> and <paramref name="language"/>.
    /// </summary>
    /// <param name="sourceCode"> The source code to format. </param>
    /// <param name="language"> The language to use when formatting the source code. </param>
    /// <returns> The HTML string for the given <paramref name="sourceCode"/> and <paramref name="language"/>. </returns>
    string? GetHtmlString(
        string sourceCode,
        ILanguage language
    );
}
