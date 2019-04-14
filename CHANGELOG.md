# Changelog
## v1.0.3
### Summary


### Add
 N/A 


### Changes
*  add links to summary sentences


### Fixed
 N/A 


### Other Commits
* correct casing on folder name




## v1.0.2
### Summary
just a minor update to fix  [13](https://github.com/igloo15/MarkdownGenerator/issues/13). 

### Add
 N/A 


### Changes
 N/A 


### Fixed
*  There is an unchecked index on an array that may cause an error while parsing xml documentation it is now fixed this is in relation to  [13](https://github.com/igloo15/MarkdownGenerator/issues/13)


### Other Commits
* Merge branch 'develop'
* update changelog
* changlog settings is back to normal




## v1.0.1
### Summary


### Add
*  link to release notes added to packages


### Changes
*  Update build scripts
*  make appveyor only build master branch


### Fixed
*  multiple fixes related to getting summary information from comments when dealing with generics   [12](https://github.com/igloo15/MarkdownGenerator/issues/12) [11](https://github.com/igloo15/MarkdownGenerator/issues/11)


### Other Commits
* Merge branch 'develop'




## v1.0.0
### Summary


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


### Changes
*  namespace changed from starting with Igloo15 to igloo15 to match package name
*  update readme documentation
*  Update Build Scripts
*  Root and Namespace Page has new sections for Types and Summary
*  only getting exported types now


### Fixed
*  build steps are fixed and updated
*  Methods and constructors containing generic types now match correctly to comments
*  Methods and constructors containing out variables now match correctly to comments
*  Method and Constructor summaries are now correct based on parameter types and parameter counts
*  dll referencing is now semi fixed by searching in local dir
*  duplicate dlls now fixed you can no longer load dlls with the same file name
*  don't process dirs where index is less than 0
*  add nuget apikey


### Other Commits
* Merge branch 'develop'




## v0.4.1
### Summary


### Add
*  Stub comments for all public types


### Changes
*  Update documentation


### Fixed
 N/A 


### Other Commits
 N/A 




## v0.4.0
### Summary


### Add
*  Create a Usage.txt and update readme with usage text
*  Enums now properly show value
*  Logging is now added to core library and console implementation of logging is done for tool
*  MarkdownApiGenerator now accepts a ILoggerFactory inorder to support logging
*  Markdown pages now show properly parameters that are actually  an out var
*  AbstractType to deduplicate code
*  Namespace filter now an configuration option
* s configurable parameter names is now possible
* s base documentation as an example
*  Namespace Breadcrumbs
*  Implemented interfaces
*  new constructor section in documentation
*  DefaultOptions Class to handle options for default theme output
*  TypeWrapper to handle fullname, and name as well unique id construction for all classes


### Changes
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
*  now use module version id and type metadatatoken to denote each markdowntype item for better uniquness
*  turned default page builders into public classes so they can be reused
*  Method names now have parameters with links
*  Properties now have current names if they are special [] parameters
*  add special text to make it look nicer per original code
*  Type names look much nicer now and include generics
*  Type names now have links and their generic arguments may have links too
*  Type names will also potentially link back to microsoft api documentation


### Fixed
*  array now show correctly
*  broken links are now fixed


### Other Commits
* Remove empty project that is unused
* Finished initial pages for Type, Enum, Namespace, Project
* Added support for linking to C# reference documents
* Added InternalMarkdownItem interface to set location and filename
* Added GetId and BuildPage to make it easier to build specific things
* Fixed local links
* Major Refactoring moving to MarkdownApi name
* New theme system
* New Core Library separating tool and processing library
* Greatly Simplifying process
* New tester project to test core functionality
* Added better relative links
* Refactoring to allow for cleaner looking documentation
* Fix some issues with parsing enums and links
* Got it working again but theme is not complete
* Still broken but close to refactoring to Theming
* Testing to see if works now
* Refactoring for themes Currently broken
* Moving stuff out of Markable Classes and into themes
* Moving documenting building to separate classes preparing for Theming
* Created new Interfaces library to provide support for plugin based Theming eventually
* Another example
* Update Readme
* Major refactoring to make it more extensible and support theming
* Update README
* change to markdownapi
* update code to new namespace
* Update Program to use CommandLineParser
* Add new options to change output




## v0.2.0
### Summary


### Add
 N/A 


### Changes
 N/A 


### Fixed
 N/A 


### Other Commits
* Fix up building process
* Refactor Repo to include build scripts
* Refactor Code to build a net core app
* update
* Merge pull request #9 from hr-kapanakov/master
* Fix #8 Generic method comment is ignored
* Fix #8 Generic method comment is ignored
* Merge pull request #7 from nibblesnbits/master
* Fixed regex in see/paramref parsing; Added ref linking
* fixed namespace check in ResolveSeeElement
* fixed regex in see/paramref parsing
* Merge pull request #6 from nibblesnbits/master
* Added bits to handle <see> and <(type)paramref> elements in summaries
* added bits to handle <see> and <(type)paramref> elements in summaries
* Merge pull request #5 from KonH/master
* Proposed solution to filter document content by namespaces
* https://github.com/neuecc/MarkdownGenerator/issues/4
* Namespaces regex matcher added;
* support static fields
* Merge branch 'master' of https://github.com/neuecc/MarkdownGenerator
* # Conflicts:
* #	README.md
* rename proj
* Update README.md
* Wiki Gen
* changed a rule
* gen home
* Rename License. to LICENSE
* Create License.
* Create README.md
* initial commit





