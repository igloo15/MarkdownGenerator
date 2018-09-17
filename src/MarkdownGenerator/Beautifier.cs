using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Igloo15.MarkdownGenerator
{
    public static class Beautifier
    {
        public static string BeautifyType(Type t, bool isFull = false)
        {
            if (t == null) return "";
            if (t == typeof(void)) return "void";
            if (!t.IsGenericType) return
                    ((isFull) ? t.FullName : t.Name)
                    ;

            var innerFormat = string.Join(", ", t.GetGenericArguments().Select(x => BeautifyType(x)));
            return Regex.Replace(isFull ? t.GetGenericTypeDefinition().FullName : t.GetGenericTypeDefinition().Name, @"`.+$", "") + "<" + innerFormat + ">";
        }

        public static string BeautifyTypeWithLink(Type t, string link, bool isFull = false)
        {
            if (t == null) return "";
            if (t == typeof(void)) return "void";
            if (!t.IsGenericType)
                if (t.IsArray)
                {
                    return
                        $"[`{((isFull) ? t.FullName : t.Name).Replace("[]","")}`]({link})`[]`"
                        ;
                }
                else
                {
                    return
                        $"[`{((isFull) ? t.FullName : t.Name)}`]({link})"
                        ;

                }

            var innerFormat = string.Join(", ", t.GetGenericArguments().Select(x => BeautifyTypeWithLink(x, link)));
            return Regex.Replace(isFull ? t.GetGenericTypeDefinition().FullName : t.GetGenericTypeDefinition().Name, @"`.+$", "") + "<" + innerFormat + ">";
        }

        public static string ToMarkdownMethodInfo(MethodInfo methodInfo,Func<Type,string> generateRelativeLinkPath)
        {
            var isExtension = methodInfo.GetCustomAttributes<System.Runtime.CompilerServices.ExtensionAttribute>(false).Any();

            var seq = methodInfo.GetParameters().Select(x =>
            {
                var suffix = x.HasDefaultValue ? (" = " + (x.DefaultValue ?? $"null")) : "";
                return $"{BeautifyTypeWithLink(x.ParameterType, generateRelativeLinkPath(x.ParameterType))} " + x.Name + suffix;
            });

            return $"[{methodInfo.Name}]({methodInfo.DeclaringType.Name}/{methodInfo.MetadataToken}.md)" + "(" + (isExtension ? "this " : "") + string.Join(", ", seq) + ")";
        }
    }
}
