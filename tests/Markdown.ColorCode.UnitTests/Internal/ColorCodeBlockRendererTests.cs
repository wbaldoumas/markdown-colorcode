namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class ColorCodeBlockRendererTests : HtmlObjectRenderer<CodeBlock>
{
    private const string MarkdownWithLanguage = @"
# Here is a header

```csharp
namespace Markdown.ColorCode;

/// <summary>
///     Just a temporary mock class to aid in bootstrapping the library.
/// </summary>
public class Foo
{
    /// <summary>
    ///     Just a temporary mock method to aid in bootstrapping the library.
    /// </summary>
    /// <returns>Bar</returns>
    public string Bar() => ""Bar"";

    public int Baz()
    {
       Console.WriteLine(Bar());
       
       return 12345;
    };
}
```

That was some **code**.
";

    private const string MarkdownWithoutLanguage = @"
# Here is a header

```
namespace Markdown.ColorCode;

/// <summary>
///     Just a temporary mock class to aid in bootstrapping the library.
/// </summary>
public class Foo
{
    /// <summary>
    ///     Just a temporary mock method to aid in bootstrapping the library.
    /// </summary>
    /// <returns>Bar</returns>
    public string Bar() => ""Bar"";

    public int Baz()
    {
       Console.WriteLine(Bar());
       
       return 12345;
    };
}
```

That was some **code**.
";

    private const string MarkdownWithUnsupportedLanguage = @"
# Here is a header

```elixir
def exclaim(string) do
  string
    |> String.trim()
    |> String.capitalize()
    |> Kernel.<>(""!"")
end
```

That was some **code**.
";

    private const string MarkdownWithoutFencedCodeBlock = @"
# Here is a header

`var test = 123456789;`

That was some **code**.
";

    private const string MarkdownWithEmptyFencedCodeBlockInline =
        "To print \"Hello World\" in C#, you can use the following:\n```c";

    private const string MarkdownWithEmptyFencedCodeBlock = @"
# Here is a header
```";

    private const string MarkdownWithEmptyFencedCodeBlockAndLanguage = @"
# Here is a header
```csharp";

    private const string MarkdownWithEmptyClosedFencedCodeBlockAndLanguage = @"
# Here is a header
```csharp
```";

    private static readonly MarkdownPipeline Pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseColorCode()
        .Build();

    private static readonly MarkdownPipeline CssPipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseColorCode(HtmlFormatterType.Css)
        .Build();

    private static readonly MarkdownPipeline DefaultLanguagePipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseColorCode(HtmlFormatterType.Style, StyleDictionary.DefaultDark, Enumerable.Empty<ILanguage>(), "csharp")
        .Build();

    private static IEnumerable<TestCaseData> TestCases
    {
        get
        {
            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlock,
                Pipeline,
                new[] { "pre", "code" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlockInline,
                Pipeline,
                new[] { "pre", "div", "style=\"color" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlockAndLanguage,
                Pipeline,
                new[] { "pre", "div", "style=\"color" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyClosedFencedCodeBlockAndLanguage,
                Pipeline,
                new[] { "pre", "div", "style=\"color" }
            );

            yield return new TestCaseData(
                MarkdownWithLanguage,
                Pipeline,
                new[] { "pre", "span", "div", "style=\"color", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithoutLanguage,
                Pipeline,
                new[] { "pre", "code", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithUnsupportedLanguage,
                Pipeline,
                new[] { "pre", "code", "capitalize", "class=\"language-elixir\"" }
            );

            yield return new TestCaseData(
                MarkdownWithoutFencedCodeBlock,
                Pipeline,
                new[] { "code", "var test = 123456789;" }
            );

            yield return new TestCaseData(
                MarkdownWithUnsupportedLanguage,
                CssPipeline,
                new[] { "pre", "code", "capitalize", "class=\"language-elixir\"" }
            );

            yield return new TestCaseData(
                MarkdownWithoutLanguage,
                CssPipeline,
                new[] { "pre", "code", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlock,
                CssPipeline,
                new[] { "pre", "code" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlockInline,
                CssPipeline,
                new[] { "pre", "div", "class=\"cplusplus\"" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlockAndLanguage,
                CssPipeline,
                new[] { "pre", "div", "class=\"csharp\"" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyClosedFencedCodeBlockAndLanguage,
                CssPipeline,
                new[] { "pre", "div", "class=\"csharp\"" }
            );

            yield return new TestCaseData(
                MarkdownWithLanguage,
                CssPipeline,
                new[] { "pre", "span", "div", "class=\"csharp\"", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithoutLanguage,
                DefaultLanguagePipeline,
                new[] { "div", "pre", "span", "style=\"color", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithUnsupportedLanguage,
                DefaultLanguagePipeline,
                new[] { "div", "pre", "span", "style=\"color", "capitalize" }
            );

            yield return new TestCaseData(
                MarkdownWithLanguage,
                DefaultLanguagePipeline,
                new[] { "div", "pre", "span", "style=\"color", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithoutLanguage,
                DefaultLanguagePipeline,
                new[] { "div", "pre", "span", "style=\"color", "12345" }
            );

            yield return new TestCaseData(
                MarkdownWithUnsupportedLanguage,
                DefaultLanguagePipeline,
                new[] { "div", "pre", "span", "style=\"color", "capitalize" }
            );

            yield return new TestCaseData(
                MarkdownWithoutFencedCodeBlock,
                DefaultLanguagePipeline,
                new[] { "code", "var test = 123456789;" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlock,
                DefaultLanguagePipeline,
                new[] { "pre", "style=\"color" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyFencedCodeBlockAndLanguage,
                DefaultLanguagePipeline,
                new[] { "pre", "div", "style=\"color" }
            );

            yield return new TestCaseData(
                MarkdownWithEmptyClosedFencedCodeBlockAndLanguage,
                DefaultLanguagePipeline,
                new[] { "pre", "div", "style=\"color" }
            );
        }
    }

    [Test]
    [TestCaseSource(nameof(TestCases))]
    public void When_given_valid_markdown_pipeline_generates_valid_html(
        string markdown,
        MarkdownPipeline pipeline,
        string[] expectedElements)
    {
        // act
        var html = Markdig.Markdown.ToHtml(markdown, pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();

        htmlDocument.LoadHtml(html);
        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll(expectedElements);
    }

    [Test]
    public void When_code_block_is_not_a_fenced_code_block_renderer_uses_internal_renderer()
    {
        // arrange
        var mockUnderlyingCodeBlockRenderer = Substitute.For<CodeBlockRenderer>();
        var mockTextWriter = Substitute.For<TextWriter>();
        var mockHtmlRenderer = Substitute.For<HtmlRenderer>(mockTextWriter);
        var mockCodeBlockParser = Substitute.For<BlockParser>();
        var mockCodeBlock = Substitute.For<CodeBlock>(mockCodeBlockParser);

        var colorCodeBlockRenderer = new ColorCodeBlockRenderer(
            mockUnderlyingCodeBlockRenderer,
            new LanguageExtractor(Enumerable.Empty<ILanguage>(), string.Empty),
            new CodeExtractor(),
            new HtmlStyleFormatter(StyleDictionary.DefaultDark)
        );

        // act
        colorCodeBlockRenderer.Write(mockHtmlRenderer, mockCodeBlock);

        // assert
        mockUnderlyingCodeBlockRenderer.ReceivedWithAnyArgs(1).Write(default!, default!);
    }

    protected override void Write(HtmlRenderer renderer, CodeBlock obj)
    {
        // Nothing to see here. 🙈
        //
        // This is needed from the hack of this test inheriting from HtmlObjectRenderer<CodeBlock> to allow
        // assertions on the protected .Write(..., ...) method of CodeBlockRenderer.
    }
}
