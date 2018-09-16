Igloo15.MarkdownGenerator
===
Generate markdown from C# binary & xml document for GitHub/GitLab Wiki.

## Install

```
dotnet tools install -g Igloo15.MarkdownGenerator
```

## Usage

```
dotnet markdownapi {dllPath(s)} {outputPath}
```

### Examples

```
dotnet markdownapi "./build/*.dll" "./docs/api"
```

```
dotnet markdownapi "./bin/MyDll.dll" "./api/markdown"
```

Put .xml on same directory, use document comment for generate.

