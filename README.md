igloo15.MarkdownApi
===
Generate markdown from C# binary & xml document for GitHub/GitLab Wiki. This library has two options

## Install

```
dotnet tools install -g igloo15.MarkdownApi.Tool
```
or 
```
nuget install igloo15.MarkdownApi.Core
```


## Usage

```
dotnet markdownapi {dllPath(s)} {outputPath}
```

### Examples

#### Tool Commands

```
markdownapi "./build/*.dll" "./docs/api"
```

```
markdownapi "./bin/MyDll.dll" "./api/markdown"
```

#### Library Snippet

```csharp
var project = MarkdownApiGenerator.GenerateProject(@"..\..\..\MarkdownApi.Core\Debug\netstandard2.0\*.dll", "");

project.Build(new DefaultTheme(new DefaultOptions
        {
            BuildNamespacePages = true,
            BuildTypePages = true,
            RootFileName = "README.md",
            RootTitle = "API",
            ShowParameterNames = true
        }
    ),
    @"..\..\..\..\docs\api"
);
```
#### Results

See the documentation result [here](./docs/api)

