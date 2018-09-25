using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Themes.Default
{
    public static class Cleaner
    {
        public static string CreateFullTypeWithLinks(IMarkdownItem currentItem, Type target, bool useFullName, bool useSpecialText)
        {
            StringBuilder sb = new StringBuilder();
            var genericArray = target.GetGenericArguments();
            for(var i = 0; i < genericArray.Length; i++)
            {
                var link = CreateFullTypeWithLinks(currentItem, genericArray[i], useFullName, useSpecialText);
                sb.Append(link);

                if (i + 1 != genericArray.Length)
                    sb.Append(", ");
            }

            var actualName = currentItem.GetNameOrNameLink(target, useFullName, useSpecialText);

            if(genericArray.Length > 0)
                return $"{actualName}\\<{sb.ToString()}>";
            return actualName;
        }

        public static string CreateFullMethodWithLinks(IMarkdownItem currentItem, MarkdownMethod method, bool useFullName, bool useParameterNames)
        {
            var parameters = method.InternalItem.GetParameters();
            MarkdownBuilder mb = new MarkdownBuilder();
            if (method.FileName != null)
                mb.Link(method.Name, currentItem.To(method));
            else
                mb.Append(method.Name);
            mb.Append(" ( ");
            if (parameters.Length > 0)
            {
                
                StringBuilder sb = new StringBuilder();
                for(var i = 0; i < parameters.Length; i++)
                {
                    var type = parameters[i].ParameterType;
                    var link = CreateFullTypeWithLinks(currentItem, type, useFullName, true);
                    sb.Append(link);

                    if(useParameterNames)
                        sb.Append($" {parameters[i].Name}");

                    if (i + 1 != parameters.Length)
                        sb.Append(", ");
                }
                mb.Append(sb.ToString());
            }
            mb.Append(" )");
            return mb.ToString();
        }

        public static string CreateFullParameterWithLinks(IMarkdownItem currentItem, MarkdownProperty property, bool useFullName, bool useParameterNames)
        {
            var fullParameterName = property.InternalItem.ToString();
            MarkdownBuilder mb = new MarkdownBuilder();
            if (property.FileName != null)
                mb.Link(property.Name, currentItem.To(property));
            else
                mb.Append(property.Name);

            var parts = fullParameterName.Split('[', ']');

            if (parts.Length > 1)
            {
                mb.Append(" [ ");

                var link = currentItem.GetLink(parts[1]);

                if (link == null)
                    mb.Append(parts[1].GetBaseName());
                else
                    mb.Link(parts[1].GetBaseName().WrapSpecial(), link);

                mb.Append(" ]");
            }
            
            return mb.ToString();
        }

        public static string CleanFullName(Type t, bool keepGenericNumber, bool specialText)
        {
            if (t == null) return "";
            if (t == typeof(void))
                return "void";


            var name = t.FullName;

            return CleanName(t.FullName, keepGenericNumber, specialText);            
        }

        public static string CleanName(string name, bool keepGenericNumber, bool specialText)
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

            if (specialText)
                return $"`{name}`";
            return name;
        }

        

    }
}
