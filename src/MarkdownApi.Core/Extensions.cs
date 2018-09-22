using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    internal static class Extensions
    {
        public static string GetId(this IMarkdownItem item)
        {
            switch(item)
            {
                case MarkdownNamespace nameItem:
                    return $"{nameItem.FullName}";
                case MarkdownProperty prop:
                    return $"{prop.ParentType.FullName}-{prop.InternalItem.MetadataToken}";
                case MarkdownType type:
                    return $"{type.FullName}";
                case MarkdownEvent eventItem:
                    return $"{eventItem.ParentType.FullName}-{eventItem.InternalItem.MetadataToken}";
                case MarkdownField field:
                    return $"{field.ParentType.FullName}-{field.InternalItem.MetadataToken}";
                case MarkdownMethod method:
                    return $"{method.ParentType.FullName}-{method.InternalItem.MetadataToken}";
                default:
                    return item.FullName;
            }
                
        }

        public static string BuildPage(this IMarkdownItem item, ITheme theme)
        {
            switch (item)
            {
                case MarkdownNamespace nameItem:
                    return theme.BuildPage(nameItem);
                case MarkdownProperty prop:
                    return theme.BuildPage(prop);
                case MarkdownType type:
                    return theme.BuildPage(type);
                case MarkdownEvent eventItem:
                    return theme.BuildPage(eventItem);
                case MarkdownField field:
                    return theme.BuildPage(field);
                case MarkdownMethod method:
                    return theme.BuildPage(method);
            }

            return "";
        }

        public static void SetLocation(this IMarkdownItem item, string value)
        {
            switch (item)
            {
                case MarkdownNamespace nameItem:
                    nameItem.Location = value;
                    break;
                case MarkdownProperty prop:
                    prop.Location = value;
                    break;
                case MarkdownType type:
                    type.Location = value;
                    break;
                case MarkdownEvent eventItem:
                    eventItem.Location = value;
                    break;
                case MarkdownField field:
                    field.Location = value;
                    break;
                case MarkdownMethod method:
                    method.Location = value;
                    break;
            }
        }

        public static void SetFileName(this IMarkdownItem item, string value)
        {
            switch (item)
            {
                case MarkdownNamespace nameItem:
                    nameItem.FileName = value;
                    break;
                case MarkdownProperty prop:
                    prop.FileName = value;
                    break;
                case MarkdownType type:
                    type.FileName = value;
                    break;
                case MarkdownEvent eventItem:
                    eventItem.FileName = value;
                    break;
                case MarkdownField field:
                    field.FileName = value;
                    break;
                case MarkdownMethod method:
                    method.FileName = value;
                    break;
            }
        }

        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> array, Action<T> doThis)
        {
            foreach (var item in array)
            {
                doThis(item);
            }

            return array;
        }

        public static IEnumerable<T> Together<T>(this IEnumerable<T> arrayOne, IEnumerable<T> arrayTwo)
        {
            foreach (var itemOne in arrayOne)
            {
                yield return itemOne;
            }

            foreach (var itemTwo in arrayTwo)
            {
                yield return itemTwo;
            }
        }

        #region GetTypeParts

        public static MarkdownMethod[] GetMethods(this MarkdownType mdType)
        {
            return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(mi => new MarkdownMethod(mi, false))
                .ToArray();
        }

        public static MarkdownMethod[] GetStaticMethods(this MarkdownType mdType)
        {
            return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(mi => new MarkdownMethod(mi, true))
                .ToArray();
        }

        public static MarkdownProperty[] GetProperties(this MarkdownType mdType)
        {
            return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Where(y => y.FilterPropertyInfo())
                .Select(pi => new MarkdownProperty(pi, false))
                .ToArray();
        }

        public static MarkdownProperty[] GetStaticProperties(this MarkdownType mdType)
        {
            return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Where(y => y.FilterPropertyInfo())
                .Select(pi => new MarkdownProperty(pi, true))
                .ToArray();
        }

        public static MarkdownField[] GetFields(this MarkdownType mdType)
        {
            return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(fi => new MarkdownField(fi, false))
                .ToArray();
        }

        public static MarkdownField[] GetStaticFields(this MarkdownType mdType)
        {
            return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(fi => new MarkdownField(fi, true))
                .ToArray();
        }

        public static MarkdownEvent[] GetEvents(this MarkdownType mdType)
        {
            return mdType.InternalType.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Select(ei => new MarkdownEvent(ei, false))
                .ToArray();
        }

        public static MarkdownEvent[] GetStaticEvents(this MarkdownType mdType)
        {
            return mdType.InternalType.GetEvents(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Select(ei => new MarkdownEvent(ei, true))
                .ToArray();
        }

        private static bool FilterMemberInfo(this MemberInfo info)
        {
            return !info.GetCustomAttributes<ObsoleteAttribute>().Any();
        }

        private static bool FilterPropertyInfo(this PropertyInfo info)
        {
            var get = info.GetGetMethod(true);
            var set = info.GetSetMethod(true);
            if (get != null && set != null)
            {
                return !(get.IsPrivate && set.IsPrivate);
            }
            else if (get != null)
            {
                return !get.IsPrivate;
            }
            else if (set != null)
            {
                return !set.IsPrivate;
            }
            else
            {
                return false;
            }
        }

        #endregion
    }
}
