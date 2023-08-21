namespace Markdown.ColorCode.UnitTests.Internal;

[TestFixture]
internal sealed class CodeExtractorTests
{
    [Test]
    public void TestExtractCode()
    {
        // arrange
        var codeBlock = new CodeBlock(new FencedCodeBlockParser());

        var slice1 = new StringSlice("line1");
        var slice2 = new StringSlice("line2");
        var slice3 = new StringSlice("line3");

        codeBlock.AppendLine(ref slice1, 0, 0, 0, false);
        codeBlock.AppendLine(ref slice2, 0, 1, 1, false);
        codeBlock.AppendLine(ref slice3, 0, 2, 2, false);

        var extractor = new CodeExtractor();
        var expectedCode = "line1" + Environment.NewLine + "line2" + Environment.NewLine + "line3";

        // act
        var extractedCode = extractor.ExtractCode(codeBlock);

        // assert
        extractedCode.Should().Be(expectedCode);
    }
}
