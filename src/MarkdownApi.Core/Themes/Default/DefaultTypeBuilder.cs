using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.Interfaces;
using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// The default type page builder
    /// </summary>
    public class DefaultTypeBuilder
    {
        private DefaultOptions _options;

        /// <summary>
        /// The default type page builder constructor
        /// </summary>
        /// <param name="options">The options for this page builder</param>
        public DefaultTypeBuilder(DefaultOptions options)
        {
            _options = options;
        }

        /// <summary>
        /// Builds the page of a MarkdownType and return the rendered markdown
        /// </summary>
        /// <param name="item">The markdown item to be rendered</param>
        /// <returns>The markdown text</returns>
        public string BuildPage(MarkdownType item)
        {
            DefaultTheme.ThemeLogger?.LogDebug("Building Type Page");
            var mb = new MarkdownBuilder();
            
            mb.HeaderWithCode(1, Cleaner.CreateFullTypeWithLinks(item, item.InternalType, false, false));
            
            mb.AppendLine();
            
            item.BuildNamespaceLinks(item.Namespace, mb);

            if (_options.ShowAssembly)
                mb.Append("Assembly: ").AppendLine(item.InternalType.Module.Name).AppendLine();

            
            bool firstInterface = true;
            foreach(var interfaceItem in item.InternalType.GetInterfaces())
            {
                if (firstInterface)
                {
                    firstInterface = false;
                    mb.Append("Implements ");
                }
                else
                    mb.Append(", ");

                var link = Cleaner.CreateFullTypeWithLinks(item, interfaceItem, false, false);
                if (string.IsNullOrEmpty(link))
                {
                    mb.Link(Cleaner.CleanName(interfaceItem.Name, false, false), "");
                }
                else
                {
                    mb.Append(link);
                }
            }

            if(!firstInterface)
                mb.AppendLine().AppendLine();

            if (!string.IsNullOrEmpty(item.Summary))
            {
                mb.Header(2, "Summary");
                mb.AppendLine(item.Summary);
            }
            

            mb.AppendLine();
            var typeZeroHeaders = new[] { "Name", "Summary" };
            var typeOneHeaders = new[] { "Type", "Name", "Summary" };
            var typeTwoHeaders = new[] { "Return", "Name", "Summary" };

            BuildTable(mb, "Static Constructors", item.GetConstructors(true), typeZeroHeaders, item);
            BuildTable(mb, "Constructors", item.GetConstructors(false), typeZeroHeaders, item);

            BuildTable(mb, "Fields", item.GetFields(false), typeZeroHeaders, item);
            BuildTable(mb, "Properties", item.GetProperties(false), typeZeroHeaders, item);
            BuildTable(mb, "Methods", item.GetMethods(false), typeZeroHeaders, item);
            BuildTable(mb, "Events", item.GetEvents(false), typeZeroHeaders, item);

            BuildTable(mb, "Static Fields", item.GetFields(true), typeZeroHeaders, item);
            BuildTable(mb, "Static Properties", item.GetProperties(true), typeZeroHeaders, item);
            BuildTable(mb, "Static Methods", item.GetMethods(true), typeZeroHeaders, item);
            BuildTable(mb, "Static Events", item.GetEvents(true), typeZeroHeaders, item);

            return mb.ToString();
        }


        

        private void BuildTable(MarkdownBuilder mb, string label, IMarkdownItem[] items, string[] headers, MarkdownType mdType)
        {
            if (items.Any())
            {
                mb.Header(2, label);
                mb.AppendLine();

                var seq = items.OrderBy(x => x.Name);

                List<string[]> data = new List<string[]>();

                foreach(var item in seq)
                {
                    string[] dataValues = new string[headers.Length];


                    Type lookUpType = null;

                    if (item.ItemType == MarkdownItemTypes.Method)
                        lookUpType = item.As<MarkdownMethod>().ReturnType;
                    else if (item.ItemType == MarkdownItemTypes.Constructor)
                        lookUpType = null;
                    else
                        lookUpType = item.As<IMarkdownTypePartValue>().Type;



                    if (item.ItemType == MarkdownItemTypes.Constructor)
                        dataValues[0] = Cleaner.CreateFullConstructorsWithLinks(mdType, item.As<MarkdownConstructor>(), false, _options.ShowParameterNames);
                    else
                    {
                        dataValues[0] = Cleaner.CreateFullTypeWithLinks(mdType, lookUpType, false, false);
                    }

                    string name = item.FullName;
                    string summary = item.Summary;

                   

                    if (item.ItemType == MarkdownItemTypes.Method)
                    {
                        name = Cleaner.CreateFullMethodWithLinks(mdType, item.As<MarkdownMethod>(), false, _options.ShowParameterNames, false);
                    }
                    else if(item.ItemType == MarkdownItemTypes.Property)
                    {
                        name = Cleaner.CreateFullParameterWithLinks(mdType, item.As<MarkdownProperty>(), false, _options.ShowParameterNames);
                    }
                    else if(item.ItemType == MarkdownItemTypes.Constructor)
                    {
                        name = Cleaner.CreateFullConstructorsWithLinks(mdType, item.As<MarkdownConstructor>(), false, _options.BuildConstructorPages);
                    }

                    dataValues[0] = name;

                    if(headers.Length > 1)
                        dataValues[1] = item.Summary;

                    data.Add(dataValues);
                }

                mb.Table(headers, data, true);
                mb.AppendLine();
            }
        }
    }
}
