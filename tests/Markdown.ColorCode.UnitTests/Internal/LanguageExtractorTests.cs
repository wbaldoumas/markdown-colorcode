namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class LanguageExtractorTests
{
    private ILanguage _mockLanguage = default!;
    private ILanguageExtractor _languageExtractor = default!;

    [SetUp]
    public void SetUp()
    {
        _mockLanguage = Substitute.For<ILanguage>();

        _mockLanguage.Id.Returns("text");
        _mockLanguage.Name.Returns("Text");

        _languageExtractor = new LanguageExtractor(new[] { _mockLanguage }, "text");
    }

    [Test]
    public void When_LanguageExtractor_extracts_expected_language_it_is_returned()
    {
        // arrange
        var mockFencedCodeBlock = Substitute.For<IFencedBlock>();

        mockFencedCodeBlock.Info.Returns("language-csharp");

        var fencedCodeBlockParser = new FencedCodeBlockParser();

        // act
        var language = _languageExtractor.ExtractLanguage(mockFencedCodeBlock, fencedCodeBlockParser);

        // assert
        language.Should().NotBeNull("because the language was found");
        language!.Name.Should().Be("C#");
    }

    [Test]
    public void When_LanguageExtractor_cannot_extract_language_id_default_language_is_returned()
    {
        // arrange
        var mockFencedCodeBlock = Substitute.For<IFencedBlock>();

        mockFencedCodeBlock.Info.Returns(string.Empty);

        var fencedCodeBlockParser = new FencedCodeBlockParser();

        // act
        var language = _languageExtractor.ExtractLanguage(mockFencedCodeBlock, fencedCodeBlockParser);

        // assert
        language.Should().NotBeNull("because the language was found");
        language!.Name.Should().Be("Text");
    }

    [Test]
    public void When_LanguageExtractor_does_not_have_default_language_null_is_returned()
    {
        // arrange
        var noDefaultColorCodeLanguageExtractor = new LanguageExtractor(new[] { _mockLanguage }, string.Empty);

        var mockFencedCodeBlock = Substitute.For<IFencedBlock>();

        mockFencedCodeBlock.Info.Returns(string.Empty);

        var fencedCodeBlockParser = new FencedCodeBlockParser();

        // act
        var language = noDefaultColorCodeLanguageExtractor.ExtractLanguage(mockFencedCodeBlock, fencedCodeBlockParser);

        // assert
        language.Should().BeNull("because the language was not found");
    }
}
