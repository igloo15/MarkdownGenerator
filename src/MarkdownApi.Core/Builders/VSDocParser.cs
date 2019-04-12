﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
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
        public static XmlDocumentComment[] ParseXmlComment(XDocument xDocument) {
            return ParseXmlComment(xDocument, null);
        }

        // cheap, quick hack parser:)
        internal static XmlDocumentComment[] ParseXmlComment(XDocument xDocument, string namespaceMatch) {
            return xDocument.Descendants("member")
                .Select(x => {
                    var match = Regex.Match(x.Attribute("name").Value, @"(.):(.+)\.([^.()]+)?(\(.+\)|$)");
                    if (!match.Groups[1].Success) return null;

                    var memberType = (MemberType)match.Groups[1].Value[0];
                    if (memberType == MemberType.None) return null;

                    var summaryXml = x.Elements("summary").FirstOrDefault()?.ToString()
                        ?? x.Element("summary")?.ToString()
                        ?? "";
                    summaryXml = Regex.Replace(summaryXml, @"<\/?summary>", string.Empty);
                    summaryXml = Regex.Replace(summaryXml, @"<para\s*/>", Environment.NewLine);
                    summaryXml = Regex.Replace(summaryXml, @"<see cref=""\w:([^\""]*)""\s*\/>", m => ResolveSeeElement(m, namespaceMatch));

                    var parsed = Regex.Replace(summaryXml, @"<(type)*paramref name=""([^\""]*)""\s*\/>", e => $"`{e.Groups[1].Value}`");

                    var summary = parsed;

                    if (summary != "") {
                        summary = string.Join("  ", summary.Split(new[] { "\r", "\n", "\t" }, StringSplitOptions.RemoveEmptyEntries).Select(y => y.Trim()));
                    }

                    var returns = ((string)x.Element("returns")) ?? "";
                    var remarks = ((string)x.Element("remarks")) ?? "";

                    var parameters = x.Elements("param")
                        .Select(e => Tuple.Create(e.Attribute("name").Value, e))
                        .Distinct(new Item1EqualityCompaerer<string, XElement>())
                        .ToDictionary(e => e.Item1, e => e.Item2.Value);

                    if (memberType == MemberType.Method && match.Groups.Count > 3 && !string.IsNullOrEmpty(match.Groups[4].Value))
                    {
                        int index = 0;
                        Dictionary<string, string> methodParams = new Dictionary<string, string>();
                        var paramTypes = match.Groups[4].Value.Replace("(", "").Replace(")", "").Split(',');
                        foreach(var paramElem in x.Elements("param"))
                        {
                            methodParams.Add(paramElem.Attribute("name").Value + ":" + paramTypes[index], paramElem.Value);
                            index++;
                        }
                        parameters = methodParams;
                    }
                    

                    

                    var className = (memberType == MemberType.Type)
                        ? match.Groups[2].Value + "." + match.Groups[3].Value
                        : match.Groups[2].Value;

                    return new XmlDocumentComment {
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

        private static string ResolveSeeElement(Match m, string ns) {
            var typeName = m.Groups[1].Value;
            if (!string.IsNullOrWhiteSpace(ns)) {
                if (typeName.StartsWith(ns)) {
                    return $"[{typeName}]({Regex.Replace(typeName, $"\\.(?:.(?!\\.))+$", me => me.Groups[0].Value.Replace(".", "#").ToLower())})";
                }
            }
            return $"`{typeName}`";
        }

        class Item1EqualityCompaerer<T1, T2> : EqualityComparer<Tuple<T1, T2>>
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
    }
}
