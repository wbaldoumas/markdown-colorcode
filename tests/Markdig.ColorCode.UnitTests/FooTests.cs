using FluentAssertions;
using NUnit.Framework;

namespace Markdig.ColorCode.UnitTests;

[TestFixture]
public class FooTests
{
    [Test]
    public void Foo_returns_bar_as_expected()
    {
        // arrange
        var foo = new Foo();

        // act
        var bar = foo.Bar();

        // assert
        bar.Should().Be("Bar");
    }
}
