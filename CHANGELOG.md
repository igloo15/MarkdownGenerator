# Changelog
## Unreleased
### Add
*  Tab function added to MarkdownBuilder that allows you to add a tab to line
*  Summaries of types added to Namespace and Root pages
*  default settings file added to allow configuration of namespace summaries, root summary, root file name, and root title
*  Summaries now shown on Namespace Pages and Root Page
*  comment added to Cleaner class
*  new method to translate types into comment types for matching
*  Constructors now have summaries
*  many comments are added to classes
*  globbing is now added to find dlls
*  AppVeyor building software now


### Other Changes
*  Methods and constructors containing generic types now match correctly to comments
*  Methods and constructors containing out variables now match correctly to comments
*  Method and Constructor summaries are now correct based on parameter types and parameter counts
*  dll referencing is now semi fixed by searching in local dir
*  duplicate dlls now fixed you can no longer load dlls with the same file name
*  don't process dirs where index is less than 0
*  add nuget apikey




## v0.4.1
### Add
*  Stub comments for all public types


### Other Changes
*  Update documentation




## v0.4.0
### Other Changes
*  Update Usage
*  default root filename is now README.md
*  all MarkdownItems are now internal constructors to prevent construction outside of MarkdownApiGenerator
*  Removed useless naming of project
*  cleaned up project files for publishing nuget packages
*  cleaned out tool project and replaced content with reference to markdownapi.core. Now it works by calling markdownapi.core
*  updated cake script
*  updated readme with latest content
*  Namespaces are no longer added in the beginning but during resolve phase
*  MarkdownProject no longer has list of Namespaces
*  Documented DefaultTheme
*  Created new relativepath function to replace old one which did not work properly
*  RootFolder no longer exists in options instead you must define output folder when building or creating a project
*  moved all markdownitems into a new namespace/folder
*  create abstract classes to implement common functionality for markdownitems
*  array now show correctly
*  now use module version id and type metadatatoken to denote each markdowntype item for better uniquness
*  turned default page builders into public classes so they can be reused
*  Method names now have parameters with links
*  Properties now have current names if they are special [] parameters
*  add special text to make it look nicer per original code
*  broken links are now fixed
*  Type names look much nicer now and include generics
*  Type names now have links and their generic arguments may have links too
*  Type names will also potentially link back to microsoft api documentation


### Add
*  Create a Usage.txt and update readme with usage text
*  Enums now properly show value
*  Logging is now added to core library and console implementation of logging is done for tool
*  MarkdownApiGenerator now accepts a ILoggerFactory inorder to support logging
*  Markdown pages now show properly parameters that are actually  an out var
*  AbstractType to deduplicate code
*  Namespace filter now an configuration option
*  configurable parameter names is now possible
*  base documentation as an example
*  Namespace Breadcrumbs
*  Implemented interfaces
*  new constructor section in documentation
*  DefaultOptions Class to handle options for default theme output
*  TypeWrapper to handle fullname, and name as well unique id construction for all classes




## v0.2.0


