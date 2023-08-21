# Changelog

All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [2.0.0] - 2023-08-21

This release includes several breaking changes. See the details below for more info.

### Changed

The `UseColorCode` `MarkdownPipelineBuilder` extension method has been changed to take in additional optional parameters. These are:

| Parameter | Default | Usage |
|---|---|---|
| `htmlFormatterType` | `HtmlFormatterType.Style` | The HTML formatter type to use. Supports inline style-based (default) or CSS class-based code colorization. |
| `styleDictionary` | `StyleDictionary.DefaultDark` | The code colorization style to use. |
| `additionalLanguages` | `null` | Additional languages to augment the built-in languages provided by ColorCode-Universal. |
| `defaultLanguageId` | `null` | A default language ID to use if the language at hand can't be found within the languages provided by ColorCode-Universal (or the `additionalLanguages` which have augmented them). The default language will also be used if no language is provided for the given code block.

### Removed

- Removed the `UseColorCodeWithClassStyling` `MarkdownPipelineBuilder` extension method. If you were previously using this, pass `HtmlFormatterType.Css` to the `UseColorCode` extension method to define the same behavior.
- Various classes have been made `internal` to reduce the footprint of this package.

## [1.1.2] - 2023-08-18

### Changed

Dependency updates:
| Package | Type | Update | Change |
|---|---|---|---|
| [Markdig](https://togithub.com/lunet-io/markdig) ([source](https://togithub.com/xoofx/markdig)) | nuget | minor | `0.31.0` -> `0.32.0` |

## [1.1.1] - 2023-07-31

### Changed

Publish using .NET 7 SDK.

## [1.1.0] - 2023-07-31

### Added

- Support for CSS class based styling. ([#102](https://github.com/wbaldoumas/markdown-colorcode/pull/102))
- .NET 7 build ([#103](https://github.com/wbaldoumas/markdown-colorcode/pull/103))

### Changed

Dependency updates:
| Package | Type | Update | Change |
|---|---|---|---|
| [ColorCode.Core](https://togithub.com/CommunityToolkit/ColorCode-Universal) | nuget | patch | `2.0.14` -> `2.0.15` |
| [ColorCode.HTML](https://togithub.com/CommunityToolkit/ColorCode-Universal) | nuget | patch | `2.0.14` -> `2.0.15` |

## [1.0.4] - 2023-03-12

### Fixed

- Handle for empty code blocks ([#89](https://github.com/wbaldoumas/markdown-colorcode/pull/89))

## [1.0.3] - 2023-03-12

### Changed

- Markdig `v0.30.4` => `v0.31.0`

## [1.0.2] - 2022-12-09

### Changed

- ColorCode.Core `v2.0.13` => `v2.0.14`
- ColorCode.HTML `v2.0.13` => `v2.0.14`

## [1.0.1] - 2022-03-19

### Changed

- Markdig `v0.27.0` => `v0.28.0`

## [1.0.0] - 2021-12-10

Initial release!

See the [README](https://github.com/wbaldoumas/markdown-colorcode/tree/initial_release#readme) for more information.
