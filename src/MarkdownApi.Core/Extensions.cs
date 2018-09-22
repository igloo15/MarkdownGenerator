using Igloo15.MarkdownApi.Core.Builders;
using Igloo15.MarkdownApi.Core.Interfaces;
using Igloo15.MarkdownApi.Core.TypeParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownApi.Core
{
    internal static class Extensions
    {
        public static T As<T>(this IMarkdownItem item) where T : class
        {
            return item as T;
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

        public static string CombinePath(this string item1, params string[] items)
        {
            return item1 + Constants.PathSeparator + String.Join(Constants.PathSeparator.ToString(), items);
        }

        public static string AddRoot(this string item)
        {
            return item.Prepend("." + Constants.PathSeparator);
        }

        public static string Prepend(this string item, string prependValue)
        {
            return prependValue + item;
        }

        public static string To(this IMarkdownItem item, IMarkdownItem dest)
        {
            return item.Location.AddRoot().RelativePath(dest.Location.AddRoot()).CombinePath(dest.FileName).AddRoot();
        }

        public static string RelativePath(this string absPath, string relTo)
        {
            string[] absDirs = absPath.Split(Constants.PathSeparator, Path.DirectorySeparatorChar);
            string[] relDirs = relTo.Split(Constants.PathSeparator, Path.DirectorySeparatorChar);

            // Get the shortest of the two paths
            int len = absDirs.Length < relDirs.Length ? absDirs.Length :
            relDirs.Length;

            // Use to determine where in the loop we exited
            int lastCommonRoot = -1;
            int index;

            // Find common root
            for (index = 0; index < len; index++)
            {
                if (absDirs[index] == relDirs[index]) lastCommonRoot = index;
                else break;
            }

            // If we didn't find a common prefix then throw
            if (lastCommonRoot == -1)
            {
                throw new ArgumentException("Paths do not have a common base");
            }

            // Build up the relative path
            StringBuilder relativePath = new StringBuilder();

            // Add on the ..
            for (index = lastCommonRoot + 1; index < absDirs.Length; index++)
            {
                if (absDirs[index].Length > 0) relativePath.Append($"..{Constants.PathSeparator}");
            }

            // Add on the folders
            for (index = lastCommonRoot + 1; index < relDirs.Length - 1; index++)
            {
                relativePath.Append(relDirs[index] + Constants.PathSeparator);
            }
            relativePath.Append(relDirs[relDirs.Length - 1]);

            return relativePath.ToString();
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
