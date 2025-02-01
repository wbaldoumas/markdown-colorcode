namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class ColorCodeExtensionTests
{
    [Test]
    public void When_markdown_renderer_is_not_html_renderer_setup_is_aborted()
    {
        // arrange
        var mockTextWriter = Substitute.For<TextWriter>();
        var invalidRenderer = Substitute.For<TextRendererBase>(mockTextWriter);
        var pipeline = new MarkdownPipelineBuilder().Build();

        var colorCodeExtension = new ColorCodeExtension(
            new LanguageExtractor([], string.Empty),
            new CodeExtractor(),
            new HtmlStyleFormatter(StyleDictionary.DefaultDark)
        );

        // act
        colorCodeExtension.Setup(pipeline, invalidRenderer);

        // assert
        var colorCodeBlockRenderer = invalidRenderer.ObjectRenderers.FindExact<ColorCodeBlockRenderer>();

        colorCodeBlockRenderer.Should().BeNull("because it was never added");
    }

    [Test]
    public void When_original_code_block_renderer_is_not_present_it_is_created()
    {
        // arrange
        var mockTextWriter = Substitute.For<TextWriter>();
        var invalidRenderer = Substitute.For<TextRendererBase<HtmlRenderer>>(mockTextWriter);
        var pipeline = new MarkdownPipelineBuilder().Build();

        invalidRenderer.ObjectRenderers.TryRemove<CodeBlockRenderer>();

        var colorCodeExtension = new ColorCodeExtension(
            new LanguageExtractor([], string.Empty),
            new CodeExtractor(),
            new HtmlStyleFormatter(StyleDictionary.DefaultDark)
        );

        // act
        colorCodeExtension.Setup(pipeline, invalidRenderer);

        // assert
        var colorCodeBlockRenderer = invalidRenderer.ObjectRenderers.FindExact<ColorCodeBlockRenderer>();

        colorCodeBlockRenderer.Should().NotBeNull("because it was added with an internal CodeBlockRenderer");
    }
}
