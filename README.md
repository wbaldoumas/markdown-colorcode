# Markdown.ColorCode

## About The Project

An extension for [Markdig](https://github.com/xoofx/markdig) that adds syntax highlighting to code through the power of [ColorCode](https://github.com/CommunityToolkit/ColorCode-Universal).

## Demonstration

### Before

```
using Foo.Bar.Baz;

namespace Foo.Api;

public interface FooService {
    /// <summary>
    ///     Gets a new Foo!
    /// </summary>
    /// <returns>A new Foo</returns>
    public void GetFoo() {
        return new Foo();
    }
}
```

### After

```csharp
using Foo.Bar.Baz;

namespace Foo.Api;

public interface FooService {
    /// <summary>
    ///     Gets a new Foo!
    /// </summary>
    /// <returns>A new Foo</returns>
    public void GetFoo() {
        return new Foo();
    }
}
```

## Installation

### Package Manager

```
Install-Package Markdown.ColorCode -Version 0.1.0
```

### .NET CLI

```
dotnet add package Markdown.ColorCode --version 0.1.0
```

## Usage

To use this extension with [Markdig](https://github.com/xoofx/markdig), simply install the `Markdown.ColorCode` package use the ColorCode extension:

```cs
var pipeline = new MarkdownPipelineBuilder()
    .UseAdvancedExtensions()
    .UseColorCode()
    .Build();

var colorizedHtml = Markdig.Markdown.ToHtml(someMarkdown, pipeline);
```

## Roadmap

See the [open issues](https://github.com/wbaldoumas/markdown-colorcode/issues) for a list of proposed features (and known issues).

## Contributing

Contributions are what make the open source community such an amazing place to learn, inspire, and create. Any contributions you make are **greatly appreciated**. For detailed contributing guidelines, please see [CONTRIBUTING.md](CONTRIBUTING.md)

## License

Distributed under the `MIT License` License. See `LICENSE` for more information.

## Contact

[@wbaldoumas](https://github.com/wbaldoumas)

Project Link: [https://github.com/wbaldoumas/markdown-colorcode](https://github.com/wbaldoumas/markdown-colorcode)

## Acknowledgements

This `README` was adapted from
[https://github.com/othneildrew/Best-README-Template](https://github.com/othneildrew/Best-README-Template).
