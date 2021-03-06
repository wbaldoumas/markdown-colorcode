using ColorCode.Styling;
using FluentAssertions;
using HtmlAgilityPack;
using Markdig;
using Markdig.Parsers;
using Markdig.Renderers;
using Markdig.Renderers.Html;
using Markdig.Syntax;
using NSubstitute;
using NUnit.Framework;

namespace Markdown.ColorCode.UnitTests;

[TestFixture]
public class ColorCodeBlockRendererTests : HtmlObjectRenderer<CodeBlock>
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

    private readonly MarkdownPipeline _pipeline = new MarkdownPipelineBuilder()
        .UseAdvancedExtensions()
        .UseColorCode()
        .Build();

    [Test]
    public void When_markdown_with_specified_language_is_passed_valid_html_is_generated()
    {
        // act
        var html = Markdig.Markdown.ToHtml(MarkdownWithLanguage, _pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll("div", "pre", "span", "style=\"color", "12345");
    }

    [Test]
    public void When_markdown_without_specified_language_is_passed_valid_html_is_generated()
    {
        // act
        var html = Markdig.Markdown.ToHtml(MarkdownWithoutLanguage, _pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll("pre", "code", "12345");
    }

    [Test]
    public void When_markdown_with_unsupported_language_is_passed_valid_html_is_generated()
    {
        // act
        var html = Markdig.Markdown.ToHtml(MarkdownWithUnsupportedLanguage, _pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll("pre", "code", "capitalize");
    }

    [Test]
    public void When_markdown_without_fenced_code_block_is_passed_valid_html_is_generated()
    {
        // act
        var html = Markdig.Markdown.ToHtml(MarkdownWithoutFencedCodeBlock, _pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll("code", "var test = 123456789;");
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
            StyleDictionary.DefaultDark
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
