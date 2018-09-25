using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public static class DefaultThemeExtensions
    {
        public static string GetNameOrNameLink(this IMarkdownItem currentType, Type targetType, bool useFullName)
        {
            MarkdownBuilder tempMB = new MarkdownBuilder();

            if (targetType == null)
                return "";
            if (targetType == typeof(void))
                return "void";

            if (targetType.FullName == null && targetType.Name == null) 
                return "";
            
            var AllItems = currentType.AllItems;

            string name = targetType.Name;
            string fullName = targetType.ToString();


            if (useFullName)
                name = Cleaner.CleanName(fullName, false);
            else
                name = Cleaner.CleanName(targetType.Name.Split('.').Last(), false);


            if (AllItems.TryGetValue(fullName, out IMarkdownItem lookupItem))
            {
                tempMB.Link(name, currentType.To(lookupItem));
            }
            else if (fullName.StartsWith("System"))
            {

                tempMB.Link(name, "https://docs.microsoft.com/en-us/dotnet/api/" + Cleaner.CleanName(fullName, true));
            }
            else
            {
                tempMB.Append(name);
            }

            return tempMB.ToString();
        }

    }
}
