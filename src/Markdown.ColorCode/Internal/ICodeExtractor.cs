namespace Markdown.ColorCode.Internal;

/// <summary>
///     Extracts code strings from <see cref="LeafBlock"/>s.
/// </summary>
internal interface ICodeExtractor
{
    /// <summary>
    ///     Extracts a code string from the given <paramref name="leafBlock"/>.
    /// </summary>
    /// <param name="leafBlock">The <paramref name="leafBlock"/> to extract the code string from.</param>
    /// <returns>The extracted code string.</returns>
    string ExtractCode(LeafBlock leafBlock);
}
