using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Linq;

namespace igloo15.MarkdownApi.Core.Themes.Default
{

    internal static class DefaultThemeExtensions
    {

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

        public static string GetNameOrNameLink(this IMarkdownItem currentType, Type targetType, bool useFullName, bool specialText)
        {
            MarkdownBuilder tempMB = new MarkdownBuilder();

            if (targetType == null)
                return "";
            if (targetType == typeof(void))
                return "[Void]" + "(https://docs.microsoft.com/en-us/dotnet/api/System.Void)";

            if (targetType.FullName == null && targetType.Name == null)
                return "";

            // exceptions
            if (targetType.ToString().Equals("System.Collections.Generic.IEnumerable`1[T]") || targetType.ToString().Equals("System.Collections.Generic.IEnumerable`1[P]"))
            {
                return "[IEnumerable]" + "(https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Ienumerable)";
            }

            if (targetType.ToString().Contains("System.Func`3"))
            {
                return "[Func]" + "(https://docs.microsoft.com/en-us/dotnet/api/System.Func-3)";
            }

            string name = targetType.Name;
            string fullName = targetType.ToString();


            if (useFullName)
                name = Cleaner.CleanName(fullName, false, specialText);
            else
                name = Cleaner.CleanName(targetType.Name.GetBaseName(), false, specialText);


            var link = currentType.GetLink(new TypeWrapper(targetType));

            if (link != null)
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

        public static string GetLink(this IMarkdownItem currentItem, TypeWrapper info)
        {

            if (currentItem.Project.TryGetValue(info, out IMarkdownItem lookupItem))
            {
                return currentItem.To(lookupItem);
            }
            
            else if (info.FullName.StartsWith("System") || info.FullName.StartsWith("Microsoft"))
            {            
                return "https://docs.microsoft.com/en-us/dotnet/api/" + Cleaner.CleanName(info.FullName, true, false);
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
