using FluentAssertions;
using HtmlAgilityPack;
using NUnit.Framework;

namespace Markdig.ColorCode.UnitTests;

[TestFixture]
public class ColorCodeBlockRendererTests
{
    private const string MarkdownWithLanguage = @"
# Here is a header

```csharp
namespace Markdig.ColorCode;

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
namespace Markdig.ColorCode;

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
        var html = Markdown.ToHtml(MarkdownWithLanguage, _pipeline);

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
        var html = Markdown.ToHtml(MarkdownWithoutLanguage, _pipeline);

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
        var html = Markdown.ToHtml(MarkdownWithUnsupportedLanguage, _pipeline);

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
        var html = Markdown.ToHtml(MarkdownWithoutFencedCodeBlock, _pipeline);

        // assert
        html.Should().NotBeNull("because an html string was properly generated");

        var htmlDocument = new HtmlDocument();
        htmlDocument.LoadHtml(html);

        htmlDocument.ParseErrors.Should().BeEmpty("because valid html was generated");

        html.Should().ContainAll("code", "var test = 123456789;");
    }
}
