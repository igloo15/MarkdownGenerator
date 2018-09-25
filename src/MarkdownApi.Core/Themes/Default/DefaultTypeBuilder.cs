using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    internal static class DefaultTypeBuilder
    {
        internal static string BuildPage(MarkdownType item)
        {
            var mb = new MarkdownBuilder();

            mb.HeaderWithCode(1, Cleaner.CreateFullTypeWithLinks(item, item.InternalType, true));
            mb.AppendLine();
            
            if (!String.IsNullOrEmpty(item.Summary))
            {
                mb.AppendLine(item.Summary);
            }

            //mb.Append(GetCode(value));


            mb.AppendLine();

            var typeOneHeaders = new[] { "Type", "Name", "Summary" };
            var typeTwoHeaders = new[] { "Return", "Name", "Summary" };

            BuildTable(mb, "Fields", item.GetFields(false), typeOneHeaders, item);
            BuildTable(mb, "Properties", item.GetProperties(false), typeOneHeaders, item);
            BuildTable(mb, "Methods", item.GetMethods(false), typeTwoHeaders, item);
            BuildTable(mb, "Events", item.GetEvents(false), typeOneHeaders, item);

            BuildTable(mb, "Static Fields", item.GetFields(true), typeOneHeaders, item);
            BuildTable(mb, "Static Properties", item.GetProperties(true), typeOneHeaders, item);
            BuildTable(mb, "Static Methods", item.GetMethods(true), typeTwoHeaders, item);
            BuildTable(mb, "Static Events", item.GetEvents(true), typeOneHeaders, item);


            return mb.ToString();
        }

        

        private static void BuildTable(MarkdownBuilder mb, string label, IMarkdownItem[] items, string[] headers, MarkdownType mdType)
        {
            if (items.Any())
            {
                mb.Header(2, label);
                mb.AppendLine();

                var seq = items.OrderBy(x => x.Name);

                List<string[]> data = new List<string[]>();

                foreach(var item in seq)
                {
                    string[] dataValues = new string[3];


                    Type lookUpType = null;

                    if (item.ItemType == MarkdownItemTypes.Method)
                        lookUpType = item.As<MarkdownMethod>().ReturnType;
                    else
                        lookUpType = item.As<IMarkdownTypePartValue>().Type;



                    dataValues[0] = Cleaner.CreateFullTypeWithLinks(mdType, lookUpType, false);

                    dataValues[1] = item.FullName;

                    dataValues[2] = item.Summary;

                    data.Add(dataValues);
                }

                mb.Table(headers, data);
                mb.AppendLine();
            }
        }
    }
}
