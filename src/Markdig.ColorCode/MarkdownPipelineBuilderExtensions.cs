using ColorCode.Styling;

namespace Markdig.ColorCode;

/// <summary>
///     Extensions for adding ColorCode to Markdig.
/// </summary>
public static class MarkdownPipelineBuilderExtensions
{
    /// <summary>
    ///     Use ColorCode to apply code colorization.
    /// </summary>
    /// <param name="pipeline">The pipeline the ColorCode extension is being added to.</param>
    /// <param name="styleDictionary">An optional StyleDictionary for custom styling.</param>
    /// <returns>The MarkdownPipelineBuilder with the added ColorCode extension.</returns>
    public static MarkdownPipelineBuilder UseColorCode(
        this MarkdownPipelineBuilder pipeline,
        StyleDictionary? styleDictionary = null)
    {
        pipeline.Extensions.Add(new ColorCodeExtension(styleDictionary ?? StyleDictionary.DefaultDark));

        return pipeline;
    }
}
