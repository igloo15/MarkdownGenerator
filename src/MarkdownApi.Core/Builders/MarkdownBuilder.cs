using System.Collections.Generic;
using System.Text;

namespace Igloo15.MarkdownApi.Core.Builders
{
    public class MarkdownBuilder
    {
        public static string MarkdownCodeQuote(string code)
        {
            return "`" + code + "`";
        }


        StringBuilder sb = new StringBuilder();

        public MarkdownBuilder Append(string text)
        {
            sb.Append(text);

            return this;
        }

        public MarkdownBuilder AppendLine()
        {
            sb.AppendLine();

            return this;
        }

        public MarkdownBuilder AppendLine(string text)
        {
            sb.AppendLine(text);

            return this;
        }

        public MarkdownBuilder Header(int level, string text)
        {
            for (int i = 0; i < level; i++)
            {
                sb.Append("#");
            }
            sb.Append(" ");
            sb.AppendLine(text);

            return this;
        }

        public MarkdownBuilder HeaderWithCode(int level, string code)
        {
            for (int i = 0; i < level; i++)
            {
                sb.Append("#");
            }
            sb.Append(" ");
            sb.Append(code);
            sb.AppendLine();

            return this;
        }

        public MarkdownBuilder HeaderWithLink(int level, string text, string url)
        {
            for (int i = 0; i < level; i++)
            {
                sb.Append("#");
            }
            sb.Append(" ");
            Link(text, url);
            sb.AppendLine();

            return this;
        }

        public MarkdownBuilder Link(string text, string url)
        {
            sb.Append("[");
            sb.Append(text);
            sb.Append("]");
            sb.Append("(");
            sb.Append(url);
            sb.Append(")");

            return this;
        }

        public MarkdownBuilder Image(string altText, string imageUrl)
        {
            sb.Append("!");
            Link(altText, imageUrl);

            return this;
        }

        public MarkdownBuilder Code(string language, string code)
        {
            sb.Append("```");
            sb.AppendLine(language);
            sb.AppendLine(code);
            sb.AppendLine("```");

            return this;
        }

        public MarkdownBuilder CodeQuote(string code)
        {
            sb.Append("`");
            sb.Append(code);
            sb.Append("`");

            return this;
        }

        /// <summary>
        /// Create a table with the header and given items
        /// </summary>
        /// <param name="headers">The header of table</param>
        /// <param name="items">The items on each row</param>
        public MarkdownBuilder Table(string[] headers, IEnumerable<string[]> items)
        {
            sb.Append("| ");
            foreach (var item in headers)
            {
                sb.Append(item);
                sb.Append(" | ");
            }
            sb.AppendLine();

            sb.Append("| ");
            foreach (var item in headers)
            {
                sb.Append("---");
                sb.Append(" | ");
            }
            sb.AppendLine();


            foreach (var item in items)
            {
                sb.Append("| ");
                foreach (var item2 in item)
                {
                    sb.Append(item2);
                    sb.Append(" | ");
                }
                sb.AppendLine();
            }
            sb.AppendLine();

            return this;
        }

        public MarkdownBuilder List(string text) // nest zero
        {
            sb.Append("- ");
            sb.AppendLine(text);

            return this;
        }

        public MarkdownBuilder ListLink(string text, string url) // nest zero
        {
            sb.Append("- ");
            Link(text, url);
            sb.AppendLine();

            return this;
        }

        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
