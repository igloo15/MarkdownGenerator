using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Json;
using System.Text;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// The default options that can be set on the default theme
    /// </summary>
    public class DefaultOptions
    {
        /// <summary>
        /// Build namespace pages defaults to true
        /// </summary>
        public bool BuildNamespacePages { get; set; } = true;

        /// <summary>
        /// Build Type pages defaults to true
        /// </summary>
        public bool BuildTypePages { get; set; } = true;

        /// <summary>
        /// Build Method Pages defaults to false
        /// </summary>
        public bool BuildMethodPages { get; set; } = false;

        /// <summary>
        /// Build Constructor Pages defaults to false
        /// </summary>
        public bool BuildConstructorPages { get; set; } = false;

        /// <summary>
        /// Show the types on the root page defaults to true
        /// </summary>
        public bool ShowTypesOnRootPage { get; set; } = true;

        /// <summary>
        /// The name of the folder containing method pages
        /// </summary>
        public string MethodFolderName { get; set; } = "Methods";

        /// <summary>
        /// The name of the folder containing constructor pages
        /// </summary>
        public string ConstructorFolderName { get; set; } = "Constructors";

        /// <summary>
        /// The name of the root file name of the api documentation
        /// </summary>
        public string RootFileName { get; set; } = "Home.md";

        /// <summary>
        /// The title in the root file
        /// </summary>
        public string RootTitle { get; set; } = "API";

        /// <summary>
        /// The summary text provided in the root file 
        /// </summary>
        public string RootSummary { get; set; }

        /// <summary>
        /// Determines if the assembly from which the markdown page came from is shown defaults to true
        /// </summary>
        public bool ShowAssembly { get; set; } = true;

        /// <summary>
        /// Determines if the parameter names should be shown defaults to false
        /// </summary>
        public bool ShowParameterNames { get; set; } = false;

        /// <summary>
        /// Summaries for each namespace found
        /// </summary>
        public Dictionary<string, string> NamespaceSummaries { get; set; } = new Dictionary<string, string>();

        /// <summary>
        /// File to load namespace summaries from
        /// </summary>
        public string DefaultSettingsFile { get; set; } = "default.settings.json";


        internal DefaultOptions LoadFile()
        {
            if(File.Exists(DefaultSettingsFile))
            {
                var defaultSettingsObj = JsonObject.Parse(File.ReadAllText(DefaultSettingsFile)) as JsonObject;

                if(defaultSettingsObj != null)
                {
                    try
                    {
                        if (defaultSettingsObj.TryGetValue(nameof(RootSummary), out JsonValue rootSummary))
                            RootSummary = rootSummary;

                        if(defaultSettingsObj.TryGetValue(nameof(NamespaceSummaries), out JsonValue nameSpaceSummariesVal))
                        {
                            var namespaceSummaryObj = nameSpaceSummariesVal as JsonObject;
                            if(namespaceSummaryObj != null)
                            {
                                namespaceSummaryObj.Foreach(kvp => NamespaceSummaries[kvp.Key] = kvp.Value);
                            }
                        }

                        if (defaultSettingsObj.TryGetValue(nameof(RootFileName), out JsonValue rootFile))
                            RootFileName = rootFile;

                        if (defaultSettingsObj.TryGetValue(nameof(RootTitle), out JsonValue rootTitle))
                            RootTitle = rootTitle;
                    }
                    catch (Exception e)
                    {
                        Constants.Logger?.LogError(e, "Failed in loading default settings json file. File was found but it was in bad format");
                    }
                    
                }
                    
            }

            return this;
        }
    }
}
