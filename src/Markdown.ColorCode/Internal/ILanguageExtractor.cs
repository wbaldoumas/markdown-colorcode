namespace Markdown.ColorCode.Internal;

/// <summary>
///     Extracts a <see cref="ILanguage"/> from the given <see cref="IFencedBlock"/>.
/// </summary>
internal interface ILanguageExtractor
{
    /// <summary>
    ///    Extracts a <see cref="ILanguage"/> from the given <see cref="IFencedBlock"/>.
    /// </summary>
    /// <param name="fencedBlock">The <paramref name="fencedBlock"/> to extract the <see cref="ILanguage"/> from.</param>
    /// <param name="fencedCodeBlockParser">The <paramref name="fencedCodeBlockParser"/> used to aid in <see cref="ILanguage"/> extraction.</param>
    /// <returns>The extracted <see cref="ILanguage"/> or null if the language could not be extracted.</returns>
    ILanguage? ExtractLanguage(IFencedBlock fencedBlock, FencedCodeBlockParser fencedCodeBlockParser);
}
