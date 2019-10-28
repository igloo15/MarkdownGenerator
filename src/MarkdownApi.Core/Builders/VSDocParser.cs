using igloo15.MarkdownApi.Core.Themes.Default;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace igloo15.MarkdownApi.Core.Builders
{
  /// <summary>
  /// Type of Xml Comment
  /// </summary>
  public enum MemberType
  {
    /// <summary>
    /// Xml comment for field
    /// </summary>
    Field = 'F',

    /// <summary>
    /// Xml Comment for Property
    /// </summary>
    Property = 'P',

    /// <summary>
    /// Xml Comment for Type
    /// </summary>
    Type = 'T',

    /// <summary>
    /// Xml comment for Event
    /// </summary>
    Event = 'E',

    /// <summary>
    /// Xml comment for Method
    /// </summary>
    Method = 'M',

    /// <summary>
    /// Xml comment for none
    /// </summary>
    None = 0
  }

  /// <summary>
  /// Xml Comment in Xml Document
  /// </summary>
  public class XmlDocumentComment
  {
    /// <summary>
    /// The type of comment
    /// </summary>
    public MemberType MemberType { get; internal set; }

    /// <summary>
    /// The class name for the comment
    /// </summary>
    public string ClassName { get; internal set; }

    /// <summary>
    /// The Member Name for this comment
    /// </summary>
    public string MemberName { get; internal set; }

    /// <summary>
    /// The Summary comment
    /// </summary>
    public string Summary { get; internal set; }

    /// <summary>
    /// The Remarks of the comment
    /// </summary>
    public string Remarks { get; internal set; }

    /// <summary>
    /// Any parameter summaries of comment
    /// </summary>
    public Dictionary<string, string> Parameters { get; internal set; }

    /// <summary>
    /// The summary of the return
    /// </summary>
    public string Returns { get; internal set; }

    /// <summary>
    /// Converts comment to a single string summary
    /// </summary>
    /// <returns></returns>
    public override string ToString()
    {
      return MemberType + ":" + ClassName + "." + MemberName;
    }
  }

  internal static class VSDocParser
  {
    public static XmlDocumentComment[] ParseXmlComment(XDocument xDocument)
    {
      return ParseXmlComment(xDocument, null);
    }

    // cheap, quick hack parser:)
    internal static XmlDocumentComment[] ParseXmlComment(XDocument xDocument, string namespaceMatch)
    {
      return xDocument.Descendants("member")
          .Select(x =>
          {
            var match = Regex.Match(x.Attribute("name").Value, @"(.):(.+)\.([^.()]+)?(\(.+\)|$)");
            if (!match.Groups[1].Success) return null;

            var memberType = (MemberType)match.Groups[1].Value[0];
            if (memberType == MemberType.None) return null;

            var summaryXml = x.Elements("summary").FirstOrDefault()?.ToString()
                      ?? x.Element("summary")?.ToString()
                      ?? "";

            summaryXml = Regex.Replace(summaryXml, @"<\/?summary>", string.Empty);
            summaryXml = Regex.Replace(summaryXml, "<para>", "<br>"); // replace para with a new line
                  summaryXml = Regex.Replace(summaryXml, "</para>", string.Empty); // remove closing para tag

                  summaryXml = Regex.Replace(summaryXml, @"<see cref=""\w:([^\""]*)""\s*\/>", m => ResolveSeeElement(m, namespaceMatch));

            var parsed = Regex.Replace(summaryXml, @"<(type)*paramref name=""([^\""]*)""\s*\/>", e => $"`{e.Groups[2].Value}`");


            var summary = parsed;

            if (summary != "")
            {
              summary = string.Join("  ", summary.Split(new[] { "\r", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()));
            }


            var returns = x.Elements("returns").FirstOrDefault()?.ToString()
                      ?? x.Element("returns")?.ToString()
                      ?? "";

            returns = Regex.Replace(returns, @"<\/?returns>", string.Empty);

            returns = Regex.Replace(returns, "<para>", "<br>");
            returns = Regex.Replace(returns, "</para>", string.Empty);

            returns = Regex.Replace(returns, @"<see cref=""\w:([^\""]*)""\s*\/>", e => ResolveSeeElement(e, namespaceMatch));
            returns = Regex.Replace(returns, @"<(type)*paramref name=""([^\""]*)""\s*\/>", e => $"`{e.Groups[2].Value}`");

            var remarks = ((string)x.Element("remarks")) ?? "";

            var parameters = x.Elements("param")
                      .Select(e => Tuple.Create(e.Attribute("name").Value, e))
                      .Distinct(new Item1EqualityCompaerer<string, XElement>())
                      .ToDictionary(e => e.Item1, e => e.Item2.Value);

            if (memberType == MemberType.Method && match.Groups.Count > 3 && !string.IsNullOrEmpty(match.Groups[4].Value))
            {
              int index = 0;
              Dictionary<string, string> methodParams = new Dictionary<string, string>();
              var paramTypes = ParseXmlParameters(match.Groups[4].Value.Replace("(", "").Replace(")", ""));

              foreach (var paramElem in x.Elements("param"))
              {
                string newName = paramElem.Attribute("name").Value;

                if (index < paramTypes.Length)
                {
                  newName = newName + ":" + paramTypes[index].Replace("0:", "");
                }
                methodParams.Add(newName, paramElem.Value);
                index++;
              }
              parameters = methodParams;
            }

            var className = (memberType == MemberType.Type)
                      ? match.Groups[2].Value + "." + match.Groups[3].Value
                      : match.Groups[2].Value;

            return new XmlDocumentComment
            {
              MemberType = memberType,
              ClassName = className,
              MemberName = match.Groups[3].Value,
              Summary = summary.Trim(),
              Remarks = remarks.Trim(),
              Parameters = parameters,
              Returns = returns.Trim()
            };
          })
          .Where(x => x != null)
          .ToArray();
    }

    internal static XmlDocumentComment[] ParseXmlParameterComment(XDocument xDocument, string namespaceMatch)
    {
      return xDocument.Descendants("member")
          .Select(x =>
          {
            var match = Regex.Match(x.Attribute("name").Value, @"(.):(.+)\.([^.()]+)?(\(.+\)|$)");

            var parameters = x.Elements("param")
                        .Select(e => Tuple.Create(e.Attribute("name").Value, e))
                        .Distinct(new Item1EqualityCompaerer<string, XElement>())
                        .ToDictionary(e => e.Item1, e => e.Item2.ToString());

            return new XmlDocumentComment
            {
              MemberName = match.Groups[3].Value,
              Parameters = parameters,
            };
          }).Where(x => x != null)
      .ToArray();
    }

    private static int ParseParamType(string value, string[] tokens, int currIndex, List<string> newTypes)
    {
      var paramType = value;
      if (paramType.Contains("{"))
      {
        var index = paramType.IndexOf("{");
        var newType = paramType.Substring(0, index);
        var nextType = paramType.Substring(index + 1, paramType.Length - index - 1);

        List<string> innerTypes = new List<string>();
        currIndex = ParseParamType(nextType, tokens, currIndex, innerTypes);
        if (currIndex < tokens.Length && !nextType.Contains("}"))
        {
          do
          {
            paramType = tokens[currIndex];
            currIndex = ParseParamType(paramType, tokens, currIndex, innerTypes);
          }
          while (!paramType.Contains("}") && currIndex < tokens.Length);
        }

        var innerTypeValue = string.Join(",", innerTypes);

        newTypes.Add($"{newType}{{{innerTypeValue}}}");
      }
      else if (paramType.Contains("}"))
      {
        newTypes.Add(paramType.Replace("}", ""));
        currIndex++;
      }
      else
      {
        newTypes.Add(paramType);
        currIndex++;
      }

      return currIndex;
    }

    public static string ResolveSeeElement(Match m, string ns)
    {
      var typeNameMatch = Regex.Match(m.Groups[0].Value, "\"[^\"]*\"");
      var typeName = typeNameMatch.Groups[0].Value.Replace("\"", "");

      typeName = Cleaner.CleanName(typeName, true, false);

      string returned;

      // exceptions
      if (typeName.Equals("T:System.Drawing.RectangleF"))
      {
        returned = $"[{typeName.Split('.').Last()}]" + "(https://docs.microsoft.com/en-us/dotnet/api/System.Drawing.RectangleF)";
        return returned;
      }

      if (typeName.Equals("T:System.Collections.Generic.KeyNotFoundException"))
      {
        returned = $"[{typeName.Split('.').Last()}]" + "(https://docs.microsoft.com/en-us/dotnet/api/System.Collections.Generic.KeyNotFoundException-1)";
        return returned;
      }

      var displayName = $"[{typeName.Split('.').Last()}]";
      var baseLink = "(https://github.com/hargitomi97/sigstat/blob/master/docs/md/";
      string linkPath;
      if (typeName.StartsWith("F") || typeName.StartsWith("P") || typeName.StartsWith("E"))
      {
        linkPath = typeName.Replace('.', '/').Substring(typeName.IndexOf(":") + 1) + ")";
        linkPath = linkPath.Substring(0, linkPath.LastIndexOf("/")) + ".md)";
        returned = displayName + baseLink + linkPath;
      }
      else
      {
        linkPath = typeName.Replace('.', '/').Substring(typeName.IndexOf(":") + 1) + ".md)";
        returned = displayName + baseLink + linkPath;
      }

      return returned;
    }

    public class Item1EqualityCompaerer<T1, T2> : EqualityComparer<Tuple<T1, T2>>
    {
      public override bool Equals(Tuple<T1, T2> x, Tuple<T1, T2> y)
      {
        return x.Item1.Equals(y.Item1);
      }

      public override int GetHashCode(Tuple<T1, T2> obj)
      {
        return obj.Item1.GetHashCode();
      }
    }

    /// <summary>
    /// Parse Xml Parameters with support from C# Discord and Nox#8248
    /// </summary>
    /// <param name="parameterString"></param>
    /// <returns>an array of parsed parameters</returns>
    public static string[] ParseXmlParameters(string parameterString)
    {
      var newParameterList = new List<string>();
      var nestCount = 0;
      var startIdx = 0;
      for (int i = 0; i < parameterString.Length; i++)
      {
        if (parameterString[i] == ',' && nestCount == 0)
        {
          newParameterList.Add(parameterString.Substring(startIdx, i - startIdx));
          startIdx = i + 1;
        }
        else if (i == parameterString.Length - 1)
        {
          newParameterList.Add(parameterString.Substring(startIdx, parameterString.Length - startIdx));
        }
        else if (parameterString[i] == '[' || parameterString[i] == '{')
        {
          nestCount++;
        }
        else if (parameterString[i] == ']' || parameterString[i] == '}')
        {
          nestCount--;
        }
      }

      return newParameterList.ToArray();
    }

  }
}
