using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.Interfaces;
using igloo15.MarkdownApi.Core.MarkdownItems;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;

namespace igloo15.MarkdownApi.Core
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
      if (String.IsNullOrEmpty(item1))
        return String.Join(Constants.PathSeparator.ToString(), items);

      return item1 + String.Join(Constants.PathSeparator.ToString(), items);
    }

    public static string AddRoot(this string item)
    {
      return item.Prepend("." + Constants.PathSeparator);
    }

    public static string Prepend(this string item, string prependValue)
    {
      return prependValue + item;
    }

    public static string UpdatedRelativePath(this string fromPath, string toPath)
    {
      if (fromPath == toPath)
        return "";

      string[] fromDirs = fromPath.Split(new[] { Constants.PathSeparator, Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);
      string[] toDirs = toPath.Split(new[] { Constants.PathSeparator, Path.DirectorySeparatorChar }, StringSplitOptions.RemoveEmptyEntries);

      int len = Math.Min(fromDirs.Length, toDirs.Length);

      int index = 0;
      int lastCommonRoot = -1;

      for (index = 0; index < len; index++)
      {
        if (fromDirs[index] == toDirs[index]) lastCommonRoot = index;
        else break;
      }

      if (lastCommonRoot == -1)
      {
        throw new ArgumentException("Paths do not have a common base");
      }

      StringBuilder sb = new StringBuilder();

      var fromDelta = fromDirs.Length - lastCommonRoot;
      var toDelta = toDirs.Length - lastCommonRoot;

      for (var i = 1; i < fromDelta; i++)
      {
        sb.Append($"..{Constants.PathSeparator}");
      }

      for (var i = 1; i < toDelta; i++)
      {
        sb.Append(toDirs[lastCommonRoot + i]).Append(Constants.PathSeparator);
      }

      return sb.ToString();
    }

    public static string RelativePath(this string absPath, string relTo)
    {
      if (absPath == relTo)
        return "";

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
      //relativePath.Append(relDirs[relDirs.Length - 1]);

      return relativePath.ToString();
    }

    public static string GetId(this MemberInfo info)
    {
      return $"{info.Module.MetadataToken}-{info.MetadataToken}";
    }

    public static string GetCommentTypeString(this Type info)
    {
      var typeName = info.ToString().Replace("&", "@");
      if (info.GenericTypeArguments.Length > 0)
      {
        var indexDash = typeName.IndexOf('`');
        typeName = typeName.Substring(0, indexDash);
        string genericTypeString = "";
        for (var i = 0; i < info.GenericTypeArguments.Length; i++)
        {
          var genericType = info.GenericTypeArguments[i];
          genericTypeString += genericType.GetCommentTypeString();
          if (i + 1 != info.GenericTypeArguments.Length)
            genericTypeString += ",";
        }
        typeName += $"{{{genericTypeString}}}";
      }

      if (info.IsGenericParameter)
      {
        typeName = $"``{info.GenericParameterPosition}";
      }
      else if (info.IsArray)
      {
        var elementType = info.GetElementType();
        if (elementType.IsGenericParameter)
        {
          typeName = typeName.Replace(elementType.ToString(), elementType.GetCommentTypeString());
          //typeName = $"``{info.GetElementType().GenericParameterPosition}[]";
        }
        else if (IsNullableType(elementType))
        {
          typeName = typeName.Replace(elementType.ToString(), elementType.GetCommentTypeString());
        }
      }

      return typeName;
    }

    public static string GetGenericType(this string typeName)
    {
      var indexDash = typeName.IndexOf('`');
      typeName = typeName.Substring(0, indexDash);
      return typeName;
    }

    public static bool IsNullableType(this Type info)
    {
      return Nullable.GetUnderlyingType(info) != null;
    }

    public static string GetCommentName(this MethodBase info)
    {
      if (info.IsGenericMethod)
      {
        return $"{info.Name}``{info.GetGenericArguments().Count()}";
      }

      if (info.Name.StartsWith("."))
        return info.Name.Replace(".", "#");

      return $"{info.Name}";
    }
    public static int IndexOfNth(this string str, string value, int nth = 1)
    {
      if (nth <= 0)
        throw new ArgumentException("Can not find the zeroth index of substring in string. Must start with 1");
      int offset = str.IndexOf(value);
      for (int i = 1; i < nth; i++)
      {
        if (offset == -1) return -1;
        offset = str.IndexOf(value, offset + 1);
      }
      return offset;
    }

    public static bool IsMatchOnMethod(this MethodBase methodInfo, XmlDocumentComment comment)
    {
      var isCorrectType = comment.MemberName == methodInfo.GetCommentName();

      if (isCorrectType)
      {
        if (methodInfo.GetParameters().Count() == comment.Parameters.Count())
        {
          var result = methodInfo.GetParameters().All(b => comment.Parameters.ContainsKey(b.Name + ":" + b.ParameterType.GetCommentTypeString()));
          if (result)
            return result;

          return methodInfo.GetParameters().All(b => comment.Parameters.ContainsKey(b.Name));
        }
      }

      return false;
    }

    #region GetTypeParts
    public static MarkdownConstructor[] GetConstructors(this MarkdownType mdType)
    {
      return mdType.InternalType.GetConstructors(BindingFlags.Public | BindingFlags.Instance)
          .Where(x => x.FilterMemberInfo() && !x.IsPrivate)
          .Select(mi => new MarkdownConstructor(mi, mi.IsStatic))
          .ToArray();
    }

    public static MarkdownConstructor[] GetStaticConstructors(this MarkdownType mdType)
    {
      return mdType.InternalType.GetConstructors(BindingFlags.Public | BindingFlags.Static)
          .Where(x => x.FilterMemberInfo() && !x.IsPrivate)
          .Select(mi => new MarkdownConstructor(mi, mi.IsStatic))
          .ToArray();
    }

    public static MarkdownMethod[] GetMethods(this MarkdownType mdType)
    {
      return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
          .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
          .Select(mi => new MarkdownMethod(mi, false))
          .ToArray();
    }

    public static MarkdownMethod[] GetStaticMethods(this MarkdownType mdType)
    {
      return mdType.InternalType.GetMethods(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.InvokeMethod)
          .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
          .Select(mi => new MarkdownMethod(mi, true))
          .ToArray();
    }

    public static MarkdownProperty[] GetProperties(this MarkdownType mdType)
    {
      return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
          .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
          .Where(y => y.FilterPropertyInfo())
          .Select(pi => new MarkdownProperty(pi, false))
          .ToArray();
    }

    public static MarkdownProperty[] GetStaticProperties(this MarkdownType mdType)
    {
      return mdType.InternalType.GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.GetProperty | BindingFlags.SetProperty)
          .Where(x => !x.IsSpecialName && x.FilterMemberInfo())
          .Where(y => y.FilterPropertyInfo())
          .Select(pi => new MarkdownProperty(pi, true))
          .ToArray();
    }

    public static MarkdownField[] GetFields(this MarkdownType mdType)
    {
      return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
          .Where(x => !x.IsSpecialName && x.FilterMemberInfo() && !x.IsPrivate)
          .Select(fi => new MarkdownField(fi, false))
          .ToArray();
    }

    public static MarkdownField[] GetStaticFields(this MarkdownType mdType)
    {
      return mdType.InternalType.GetFields(BindingFlags.Public | BindingFlags.Static | BindingFlags.DeclaredOnly | BindingFlags.GetField | BindingFlags.SetField)
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
