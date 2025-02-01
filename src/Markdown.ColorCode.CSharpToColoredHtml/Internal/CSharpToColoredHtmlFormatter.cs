using System.Diagnostics.CodeAnalysis;
using ColorCode.Common;
using CsharpToColouredHTML.Core.Emitters.HTML;

namespace Markdown.ColorCode.CSharpToColoredHtml.Internal;

/// <inheritdoc cref="IHtmlFormatter"/>
[ExcludeFromCodeCoverage]
internal sealed class CSharpToColoredHtmlFormatter : IHtmlFormatter
{
    private readonly IHtmlFormatter _internalFormatter;

    private readonly HTMLEmitterSettings _htmlEmitterSettings;

    private readonly CsharpColourer _csharpColorer;

    /// <summary>
    ///     Create a new <see cref="CSharpToColoredHtmlFormatter"/>.
    /// </summary>
    /// <param name="internalFormatter">The internal formatter to use.</param>
    /// <param name="htmlEmitterSettings">The HTML emitter settings to use.</param>
    public CSharpToColoredHtmlFormatter(IHtmlFormatter internalFormatter, HTMLEmitterSettings htmlEmitterSettings)
    {
        _internalFormatter = internalFormatter;
        _htmlEmitterSettings = htmlEmitterSettings;
        _csharpColorer = new CsharpColourer();
    }

    /// <inheritdoc />
    public string? GetHtmlString(string sourceCode, ILanguage language) => language.Id == LanguageId.CSharp
        ? _csharpColorer.ProcessSourceCode(sourceCode, new HTMLEmitter(_htmlEmitterSettings))
        : _internalFormatter.GetHtmlString(sourceCode, language);
}
