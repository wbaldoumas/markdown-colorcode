using ColorCode;
using ColorCode.Common;

namespace Markdown.ColorCode;

/// <summary>
///     A language which does not provide any keywords
/// </summary>
internal sealed class TextLanguage : ILanguage
{
    /// <summary>
    ///     Provides the <see cref="Id"/> of this language without requiring a reference to an instance
    /// </summary>
    internal const string LanguageId = "text";

    /// <inheritdoc/>
    public string CssClassName => LanguageId;

    /// <inheritdoc/>
    public string? FirstLinePattern => null;

    /// <inheritdoc/>
    public string Id => LanguageId;

    /// <inheritdoc/>
    public string Name => nameof(TextLanguage);

    /// <inheritdoc/>
    public IList<LanguageRule> Rules => new List<LanguageRule>(1)
    {
        new ("^$", new Dictionary<int, string>(1)
        {
            { 0, ScopeName.PlainText },
        }),
    };

    /// <inheritdoc/>
    public bool HasAlias(string lang) => true;
}
