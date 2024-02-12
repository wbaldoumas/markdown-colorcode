using ColorCode.Common;
using CsharpToColouredHTML.Core;

namespace Markdown.ColorCode.Internal;

/// <inheritdoc cref="IHtmlFormatter"/>
internal sealed class CSharpToColouredHtmlFormatter : IHtmlFormatter
{
    private readonly IHtmlFormatter _internalFormatter;

    /// <summary>
    ///     Create a new <see cref="CSharpToColouredHtmlFormatter"/>.
    /// </summary>
    /// <param name="internalFormatter">The internal formatter to use.</param>
    public CSharpToColouredHtmlFormatter(IHtmlFormatter internalFormatter) => _internalFormatter = internalFormatter;

    /// <inheritdoc />
    public string? GetHtmlString(string sourceCode, ILanguage language) =>
        language.Id == LanguageId.CSharp ?
            new CsharpColourer().ProcessSourceCode(sourceCode, new HTMLEmitter(new HTMLEmitterSettings().DisableIframe().DisableLineNumbers())) :
            _internalFormatter.GetHtmlString(sourceCode, language);
}
