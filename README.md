igloo15.MarkdownApi
===

Tool: [![NuGet](https://img.shields.io/nuget/v/igloo15.MarkdownApi.Tool.svg)](https://www.nuget.org/packages/igloo15.MarkdownApi.Tool/)

Core: [![NuGet](https://img.shields.io/nuget/v/igloo15.MarkdownApi.Tool.svg)](https://www.nuget.org/packages/igloo15.MarkdownApi.Core/)



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
markdownapi --help

markdownapi 1.0.0
Copyright (C) 2019 igloo15, jyasuu, neuecc
ERROR(S):
A required value not bound to option name is missing.
USAGE:
Normal Usage:
markdownapi ./MyDll.dll ./Api
Wildcard Usage:
markdownapi ./bin/*.dll ./Api
Multiple Search Locations:
markdownapi ./bin/*.dll;./dist/myapp/myApp.dll ../../..docs/Api

  --namespace-filter           (Default: ) A regex used to generate documentation only for namespaces that match

  --root-filename              (Default: README.md) The name of the markdown file at the root of your documentation

  --title                      (Default: Api) Title of the root home page

  --summary                    (Default: ) A summary you want to appear on root page

  --namespace-page             (Default: false) Create pages for each namespace

  --type-page                  (Default: true) Create pages for each type

  --constructor-page           (Default: false) Create pages for each constructor

  --method-page                (Default: false) Create pages for each method

  --property-page              (Default: false) Create pages for each property

  --field-page                 (Default: false) Create pages for each field

  --event-page                 (Default: false) Create pages for each event

  --method-folder              (Default: Methods) The folder to store method pages in

  --constructors-folder        (Default: Constructors) The folder to store constructor pages in

  --property-folder            (Default: Properties) The folder to store property pages in

  --field-folder               (Default: Fields) The folder to store field pages in

  --event-folder               (Default: Events) The folder to store event pages in

  --theme                      (Default: Default) The theme you wish to use. Selecting a theme will potentially
                               override the commandline arguments you have defined

  --default-theme-file         (Default: default.settings.json) File containing settings for the default theme

  --help                       Display this help screen.

  --version                    Display version information.

  Dll Path (pos. 0)            Required. The path to the dll to create documentation for. May include wildcards on file
                               name. Use ';' to search multiple areas

  Output Directory (pos. 1)    (Default: md) The root folder to put documentation in
```

### Examples

#### Tool Commands

```
markdownapi "./build/**/*.dll" "./docs/api"
```
Globbing support allows you to find all dlls underneath a folder

```
markdownapi "./bin/MyDll.dll" "./api/markdown"
```
Access specific dlls and output them to a location

```
markdownapi "./dist/**/publish/igloo15*.dll" "./docs/api"
```
Many different globbing rules

#### Library Snippet

```csharp
var project = MarkdownApiGenerator.GenerateProject("../../../MarkdownApi.Core/Debug/netstandard2.0/*.dll");

project.Build(new DefaultTheme(new DefaultOptions
        {
            BuildNamespacePages = true,
            BuildTypePages = true,
            RootFileName = "README.md",
            RootTitle = "API",
            ShowParameterNames = true
        }
    ),
    "../../../../docs/api"
);
```
#### Results

See the documentation result [here](./docs/api)

