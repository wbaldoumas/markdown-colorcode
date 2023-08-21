namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class HtmlFormatterFactoryTests
{
    private StyleDictionary _styleDictionary = default!;
    private IHtmlFormatterFactory _factory = default!;

    [SetUp]
    public void Setup()
    {
        _styleDictionary = new StyleDictionary(); // Assuming appropriate construction or mock
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
        var invalidType = (HtmlFormatterType)999; // Invalid enum value

        // act
        var act = () => _factory.Get(invalidType);

        // assert
        act.Should()
            .Throw<ArgumentOutOfRangeException>()
            .And.ParamName.Should().Be("htmlFormatterType");
    }
}
