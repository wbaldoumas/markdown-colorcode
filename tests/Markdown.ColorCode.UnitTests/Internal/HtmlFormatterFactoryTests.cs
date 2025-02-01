namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class HtmlFormatterFactoryTests
{
    private StyleDictionary _styleDictionary = default!;
    private IHtmlFormatterFactory _factory = default!;

    [SetUp]
    public void Setup()
    {
        _styleDictionary = []; // Assuming appropriate construction or mock
        _factory = new HtmlFormatterFactory(_styleDictionary);
    }

    [TestCase(HtmlFormatterType.Style, typeof(HtmlStyleFormatter))]
    [TestCase(HtmlFormatterType.Css, typeof(HtmlCssFormatter))]
    public void TestGetHtmlFormatter_ValidType_ReturnsCorrectFormatter(HtmlFormatterType htmlFormatterType, Type expectedType)
    {
        // act
        var result = _factory.Get(htmlFormatterType);

        // assert
        result.Should().BeOfType(expectedType);
    }

    [Test]
    public void TestGetHtmlFormatter_InvalidType_ThrowsArgumentOutOfRangeException()
    {
        // arrange
        const HtmlFormatterType invalidType = (HtmlFormatterType)999; // Invalid enum value

        // act
        var act = () => _factory.Get(invalidType);

        // assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .And.ParamName.Should().Be("htmlFormatterType");
    }

    [Test]
    [TestCase(HtmlFormatterType.StyleWithCSharpToColoredHtml)]
    [TestCase(HtmlFormatterType.CssWithCSharpToColoredHtml)]
    public void TestGetHtmlFormatter_UnsupportedType_ThrowsNotSupportedException(HtmlFormatterType unsupportedType)
    {
        // act
        var act = () => _factory.Get(unsupportedType);

        // assert
        act.Should()
            .Throw<NotSupportedException>()
            .WithMessage($"In order to use {unsupportedType} you must install Markdown.ColorCode.CSharpToColoredHtml and invoke UseColorCodeWithCSharpToColoredHtml.");
    }
}
