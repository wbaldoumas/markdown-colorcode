namespace Markdown.ColorCode.Internal;

/// <inheritdoc cref="ILanguageExtractor"/>
internal sealed class LanguageExtractor : ILanguageExtractor
{
    private readonly string _defaultLanguageId;

    /// <summary>
    ///     Create a new <see cref="LanguageExtractor"/> with the specified <paramref name="additionalLanguages"/> and <paramref name="defaultLanguageId"/>.
    /// </summary>
    /// <param name="additionalLanguages">Additional languages used to augment the built-in languages provided by ColorCode-Universal.</param>
    /// <param name="defaultLanguageId">The default language ID. Used when the language ID cannot be extracted from the <see cref="FencedCodeBlock"/> or if the language was not found.</param>
    public LanguageExtractor(IEnumerable<ILanguage> additionalLanguages, string defaultLanguageId)
    {
        foreach (var language in additionalLanguages)
        {
            Languages.Load(language);
        }

        _defaultLanguageId = defaultLanguageId;
    }

    /// <inheritdoc />
    public ILanguage? ExtractLanguage(IFencedBlock fencedBlock, FencedCodeBlockParser fencedCodeBlockParser)
    {
        var languageId = fencedBlock.Info!.Replace(fencedCodeBlockParser.InfoPrefix!, string.Empty);
        var language = string.IsNullOrWhiteSpace(languageId) ? null : Languages.FindById(languageId);

        if (language is null && !string.IsNullOrWhiteSpace(_defaultLanguageId))
        {
            return Languages.FindById(_defaultLanguageId);
        }

        return language;
    }
}
