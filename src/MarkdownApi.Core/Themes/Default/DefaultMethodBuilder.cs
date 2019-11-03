using igloo15.MarkdownApi.Core.Builders;
using igloo15.MarkdownApi.Core.Interfaces;
using igloo15.MarkdownApi.Core.MarkdownItems.TypeParts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;
using System.Xml.Linq;

namespace igloo15.MarkdownApi.Core.Themes.Default
{
  /// <summary>
  /// Default markdown method page builder - Warning this is not yet implemented
  /// </summary>
  public class DefaultMethodBuilder
  {
    private DefaultOptions _options;


    /// <summary>
    /// Constructs a markdown method page builder
    /// </summary>
    /// <param name="options">The default options to be constructed with</param>
    public DefaultMethodBuilder(DefaultOptions options)
    {
      _options = options;
    }

    /// <summary>
    /// Builds the markdown method page content
    /// </summary>
    /// <param name="item">The markdown method item</param>
    /// <returns>The markdown content</returns>
    public string BuildPage(MarkdownMethod item)
    {
      Dictionary<string, string> parameterPairs = new Dictionary<string, string>();
      Dictionary<string, string> returnPairs = new Dictionary<string, string>();
      XmlDocumentComment[] comments = new XmlDocumentComment[0];

      MarkdownBuilder mb = new MarkdownBuilder();


      var name = Cleaner.CreateFullMethodWithLinks(item, item.As<MarkdownMethod>(), false, true, true);

      // method name + params name with type
      var FullName = item.Name + name;

      var typeZeroHeaders = new[] { "Return", "Name" };


      mb.HeaderWithLink(1, item.FullName, item.To(item));
      mb.AppendLine();


      mb.AppendLine(item.Summary);

      BuildTable(mb, item, typeZeroHeaders, item);

      mb.Append("#### Parameters");
      mb.AppendLine();

      if (File.Exists(MarkdownItemBuilder.xmlPath))
      {
        comments = VSDocParser.ParseXmlParameterComment(XDocument.Parse(File.ReadAllText(MarkdownItemBuilder.xmlPath)), "");

        foreach (var comment in comments)
        {
          foreach (var param in item.Parameters)
          {

            var foundParameterComment = comment.Parameters.FirstOrDefault(x => x.Key == param.Name).Value;
            if (foundParameterComment != null)
            {
              foundParameterComment = foundParameterComment.Substring(0, foundParameterComment.LastIndexOf('<'));
              foundParameterComment = foundParameterComment.Substring(foundParameterComment.IndexOf('>') + 1);

              var MethodName = Cleaner.CleanName(comment.MemberName, false, false);

              // method name + param name + parameter summary
              if (!parameterPairs.ContainsKey(MethodName + " " + param.Name))
                parameterPairs.Add(MethodName + " " + param.Name, foundParameterComment);
            }
          }
        }
      }

      var numberOfParameters = item.Parameters.Length;

      for(int i = 1; i<=numberOfParameters; i++)
      {
        if(i == numberOfParameters)
        ConstructParameter(mb, FullName, parameterPairs, false, i);
        else
        {
          ConstructParameter(mb, FullName, parameterPairs, true, i);
        }
      }

      mb.AppendLine();
      mb.Append("#### Returns");
      mb.AppendLine();

      Type lookUpType = null;
      if (item.ItemType == MarkdownItemTypes.Method)
        lookUpType = item.As<MarkdownMethod>().ReturnType;
      var returned = Cleaner.CreateFullTypeWithLinks(item, lookUpType, false, false);
      string foundReturnComment = string.Empty;

      if (File.Exists(MarkdownItemBuilder.xmlPath))
      {
        comments = VSDocParser.ParseXmlComment(XDocument.Parse(File.ReadAllText(MarkdownItemBuilder.xmlPath)), "");
        if (comments != null)
        {
          foreach (var k in comments)
          {
            k.MemberName = Cleaner.CleanName(k.MemberName, false, false);
            returnPairs[k.MemberName] = k.Returns;
          }
          foundReturnComment = returnPairs.FirstOrDefault(x => x.Key == item.Name).Value;
        }
      }
      foundReturnComment = Regex.Replace(foundReturnComment, @"<see cref=""\w:([^\""]*)""\s*\/>", m => VSDocParser.ResolveSeeElement(m, ""));
      mb.Append(returned);
      mb.AppendLine("<br>");
      mb.Append(foundReturnComment);

      return mb.ToString();

    }

    

    private void ConstructParameter(MarkdownBuilder mb, string FullName, Dictionary<string, string> parameterPairs, bool breakLineIndex, int number)
    {
      var MethodName = FullName.Substring(0, FullName.IndexOf(" "));
      var ParamName = string.Empty;
      if (number == 1)
      {
        ParamName = FullName.Substring(FullName.IndexOf(" "));
      }
      else
      {
        int index = Extensions.IndexOfNth(FullName, "<br>", number-1);
        ParamName = FullName.Substring(index + 4);
      }

      var ParameterTypeBegin = ParamName;
      if(ParamName.IndexOf("[") > 0)
      ParamName = ParamName.Substring(0, ParamName.IndexOf("[")).Trim();
      var methodAndParamName = MethodName + " " + ParamName;
      var ParameterComment = parameterPairs.FirstOrDefault(x => x.Key == methodAndParamName).Value;
      var BreakLineIndex = 0;

      if (ParameterComment != null)
      {
        ParamName = Cleaner.BoldName(ParamName);
        
        var ParameterType = ParameterTypeBegin.Substring(ParameterTypeBegin.IndexOf("["));
        BreakLineIndex = ParameterType.IndexOf("<br>");
        if (BreakLineIndex > 0)
          ParameterType = ParameterType.Substring(0, BreakLineIndex);
        ParameterComment = Regex.Replace(ParameterComment, @"<see cref=""\w:([^\""]*)""\s*\/>", m => VSDocParser.ResolveSeeElement(m, ""));
        if (breakLineIndex)
        {
          mb.Append(ParamName + "  " + ParameterType + "<br>" + ParameterComment + "<br><br>");
        }
        else
        {
          mb.Append(ParamName + "  " + ParameterType + "<br>" + ParameterComment);
        }
      }
    }

    private void BuildTable(MarkdownBuilder mb, IMarkdownItem item, string[] headers, MarkdownMethod mdType)
    {
      mb.AppendLine();

      List<string[]> data = new List<string[]>();


      string[] dataValues = new string[headers.Length];

      Type lookUpType = null;
      if (item.ItemType == MarkdownItemTypes.Method)
        lookUpType = item.As<MarkdownMethod>().ReturnType;

      dataValues[0] = Cleaner.CreateFullTypeWithLinks(mdType, lookUpType, false, false);

      string name = item.FullName;
      if (item.ItemType == MarkdownItemTypes.Method)
      {
        name = Cleaner.CreateFullMethodWithLinks(mdType, item.As<MarkdownMethod>(), false, true, false);
      }
      else if (item.ItemType == MarkdownItemTypes.Property)
      {
        name = Cleaner.CreateFullParameterWithLinks(mdType, item.As<MarkdownProperty>(), false, _options.ShowParameterNames);
      }
      else if (item.ItemType == MarkdownItemTypes.Constructor)
      {
        name = Cleaner.CreateFullConstructorsWithLinks(mdType, item.As<MarkdownConstructor>(), false, _options.BuildConstructorPages);
      }


      dataValues[1] = name;

      data.Add(dataValues);
      mb.Table(headers, data, true);
      mb.AppendLine();
    }
  }
}

