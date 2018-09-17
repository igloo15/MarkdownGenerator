using Igloo15.MarkdownGenerator.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace Igloo15.MarkdownGenerator
{
    internal static class Extensions
    {
        public static IEnumerable<T> Foreach<T>(this IEnumerable<T> array, Action<T> doThis)
        {
            foreach(var item in array)
            {
                doThis(item);
            }

            return array;
        }

        public static IEnumerable<T> Together<T>(this IEnumerable<T> arrayOne, IEnumerable<T> arrayTwo)
        {
            foreach(var itemOne in arrayOne)
            {
                yield return itemOne;
            }

            foreach(var itemTwo in arrayTwo)
            {
                yield return itemTwo;
            }
        }

        public static void Empty(this DirectoryInfo directory)
        {
            foreach (FileInfo file in directory.GetFiles()) file.Delete();
            foreach (DirectoryInfo subDirectory in directory.GetDirectories()) subDirectory.Delete(true);
        }

        public static string RelativePath(string absPath, string relTo)
        {
            string[] absDirs = absPath.Split(Path.DirectorySeparatorChar);
            string[] relDirs = relTo.Split(Path.DirectorySeparatorChar);

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
                if (absDirs[index].Length > 0) relativePath.Append($"..{Path.DirectorySeparatorChar}");
            }

            // Add on the folders
            for (index = lastCommonRoot + 1; index < relDirs.Length - 1; index++)
            {
                relativePath.Append(relDirs[index] + Path.DirectorySeparatorChar);
            }
            relativePath.Append(relDirs[relDirs.Length - 1]);

            return relativePath.ToString();
        }

        public static MarkdownableMethod[] GetMethods(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(mi => new MarkdownableMethod(mi, false, comments))
                .ToArray();
        }

        public static MarkdownableMethod[] GetStaticMethods(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(mi => new MarkdownableMethod(mi, true, comments))
                .ToArray();
        }

        public static MarkdownableProperty[] GetProperties(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Where(y => y.FilterPropertyInfo())
                .Select(pi => new MarkdownableProperty(pi, false, comments))
                .ToArray();
        }

        public static MarkdownableProperty[] GetStaticProperties(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Where(y => y.FilterPropertyInfo())
                .Select(pi => new MarkdownableProperty(pi, true, comments))
                .ToArray();
        }

        public static MarkdownableField[] GetFields(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(fi => new MarkdownableField(fi, false, comments))
                .ToArray();
        }

        public static MarkdownableField[] GetStaticFields(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
                .Select(fi => new MarkdownableField(fi, true, comments))
                .ToArray();
        }

        public static MarkdownableEvent[] GetEvents(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetEvents(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Select(ei => new MarkdownableEvent(ei, false, comments))
                .ToArray();
        }
        
        public static MarkdownableEvent[] GetStaticEvents(this MarkdownableType mdType, IEnumerable<XmlDocumentComment> comments)
        {
            return mdType.InternalType.GetEvents(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly)
                .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
                .Select(ei => new MarkdownableEvent(ei, true, comments))
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

    }

    
}
