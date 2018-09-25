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
        public static string GetNameOrNameLink(this IMarkdownItem currentType, Type targetType, bool useFullName, bool specialText)
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
                name = Cleaner.CleanName(fullName, false, specialText);
            else
                name = Cleaner.CleanName(targetType.Name.GetBaseName(), false, specialText);


            var link = currentType.GetLink(fullName);

            if(link != null)
            {
                tempMB.Link(name, link);
            }
            else
            {
                tempMB.Append(name);
            }
            
            return tempMB.ToString();
        }

        public static string GetLink(this IMarkdownItem currentItem, string targetTypeFullName)
        {
            if (currentItem.AllItems.TryGetValue(targetTypeFullName, out IMarkdownItem lookupItem))
            {
                return currentItem.To(lookupItem);
            }
            else if (targetTypeFullName.StartsWith("System"))
            {
                return "https://docs.microsoft.com/en-us/dotnet/api/" + Cleaner.CleanName(targetTypeFullName, true, false);
            }
            return null;
        }

        public static string WrapSpecial(this string text)
        {
            return $"`{text}`";
        }

        public static string GetBaseName(this string text)
        {
            return text.Split('.').Last();
        }

    }
}
