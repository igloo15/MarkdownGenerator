using Igloo15.MarkdownApi.Core.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public static class Cleaner
    {
        public static string CreateFullTypeWithLinks(IMarkdownItem currentItem, Type target, bool useFullName)
        {
            StringBuilder sb = new StringBuilder();
            var genericArray = target.GetGenericArguments();
            for(var i = 0; i < genericArray.Length; i++)
            {
                var link = CreateFullTypeWithLinks(currentItem, genericArray[i], useFullName);
                sb.Append(link);

                if (i + 1 != genericArray.Length)
                    sb.Append(", ");
            }

            var actualName = currentItem.GetNameOrNameLink(target, useFullName);

            if(genericArray.Length > 0)
                return $"{actualName}\\<{sb.ToString()}>";
            return actualName;
        }


        public static string CleanFullName(Type t, bool keepGenericNumber)
        {
            if (t == null) return "";
            if (t == typeof(void))
                return "void";


            var name = t.FullName;

            return CleanName(t.FullName, keepGenericNumber);            
        }

        public static string CleanName(string name, bool keepGenericNumber)
        {
            if (String.IsNullOrEmpty(name))
                return name;

            var indexBracket = name.IndexOf("[");

            if (indexBracket > 0)
                name = name.Substring(0, indexBracket);

            var indexDash = name.IndexOf('`');

            if (indexDash > 0)
            {
                if (keepGenericNumber)
                {
                    name = name.Replace('`', '-');
                }
                else
                {
                    name = name.Substring(0, indexDash);
                }
            }


            return name;
        }

        

    }
}
