using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Reflection;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    /// <summary>
    /// 
    /// </summary>
    public static class DefaultThemeExtensions
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="item"></param>
        /// <param name="namespaceValue"></param>
        /// <param name="mb"></param>
        public static void BuildNamespaceLinks(this IMarkdownItem item, string namespaceValue, MarkdownBuilder mb)
        {
            var namespaceItems = namespaceValue.Split('.');

            string globalNamespace = "";

            mb.Append("Namespace: ");

            foreach (var namespaceItem in namespaceItems)
            {
                if (!String.IsNullOrEmpty(globalNamespace))
                {
                    globalNamespace += ".";
                    mb.Append(" > ");
                }

                globalNamespace = globalNamespace + namespaceItem;

                if (item.Project.TryGetValue(new TypeWrapper(globalNamespace), out IMarkdownItem foundItem))
                {
                    mb.Link(namespaceItem, item.To(foundItem));
                }
                else
                {
                    mb.Link(namespaceItem, "");
                }
            }

            mb.AppendLine().AppendLine();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentType"></param>
        /// <param name="targetType"></param>
        /// <param name="useFullName"></param>
        /// <param name="specialText"></param>
        /// <returns></returns>
        public static string GetNameOrNameLink(this IMarkdownItem currentType, Type targetType, bool useFullName, bool specialText)
        {
            MarkdownBuilder tempMB = new MarkdownBuilder();

            if (targetType == null)
                return "";
            if (targetType == typeof(void))
                return "void";

            if (targetType.FullName == null && targetType.Name == null) 
                return "";
            
            string name = targetType.Name;
            string fullName = targetType.ToString();


            if (useFullName)
                name = Cleaner.CleanName(fullName, false, specialText);
            else
                name = Cleaner.CleanName(targetType.Name.GetBaseName(), false, specialText);


            var link = currentType.GetLink(new TypeWrapper(targetType));

            if(link != null)
            {
                tempMB.Link(name, link);
            }
            else
            {
                tempMB.Link(name, currentType.To(currentType));
            }

            if (targetType.IsArray)
                tempMB.Append("[]");
            
            return tempMB.ToString();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="currentItem"></param>
        /// <param name="info"></param>
        /// <returns></returns>
        public static string GetLink(this IMarkdownItem currentItem, TypeWrapper info)
        {
            if (currentItem.Project.TryGetValue(info, out IMarkdownItem lookupItem))
            {
                return currentItem.To(lookupItem);
            }
            else if (info.FullName.StartsWith("System"))
            {
                return "https://docs.microsoft.com/en-us/dotnet/api/" + Cleaner.CleanName(info.FullName, true, false);
            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string WrapSpecial(this string text)
        {
            return $"`{text}`";
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="text"></param>
        /// <returns></returns>
        public static string GetBaseName(this string text)
        {
            return text.Split('.').Last();
        }

    }
}
