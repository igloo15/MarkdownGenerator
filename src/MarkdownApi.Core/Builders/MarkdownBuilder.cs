using System.Collections.Generic;
using System.Text;

namespace igloo15.MarkdownApi.Core.Builders
{
  /// <summary>
  /// Builds Markdown strings
  /// </summary>
  public class MarkdownBuilder
    {

        /// <summary>
        /// Places code in a markdown codeblock
        /// </summary>
        /// <param name="code">The code to be wrapped</param>
        /// <returns>The wrapped code</returns>
        public static string MarkdownCodeQuote(string code)
        {
            return "`" + code + "`";
        }

        StringBuilder sb = new StringBuilder();

        /// <summary>
        /// Appends a Tab to the current line
        /// </summary>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder Tab()
        {
            sb.Append("\t");

            return this;
        }

        /// <summary>
        /// Appends text to an internal string builder
        /// </summary>
        /// <param name="text">The text to be appended</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder Append(string text)
        {
            sb.Append(text);

            return this;
        }

        /// <summary>
        /// Appends a new line to the internal string builder
        /// </summary>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder AppendLine()
        {
            sb.AppendLine();

            return this;
        }

        /// <summary>
        /// Appends text and a new line to internal string builder
        /// </summary>
        /// <param name="text">The text to be appended</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder AppendLine(string text)
        {
            sb.AppendLine(text);

            return this;
        }

        /// <summary>
        /// Create a header at the given level with the text
        /// </summary>
        /// <param name="level">The header level</param>
        /// <param name="text">The header text</param>
        /// <returns>The MarkdownBuilder</returns>
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

        /// <summary>
        /// Header with code
        /// </summary>
        /// <param name="level">The header level</param>
        /// <param name="code">The header code</param>
        /// <returns>The MarkdownBuilder</returns>
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

        /// <summary>
        /// The header with a link
        /// </summary>
        /// <param name="level">The header level</param>
        /// <param name="text">The header text</param>
        /// <param name="url">The header url</param>
        /// <returns>The MarkdownBuilder</returns>
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

        /// <summary>
        /// A markdown link
        /// </summary>
        /// <param name="text">The text of the link</param>
        /// <param name="url">The url of the link</param>
        /// <returns>The MarkdownBuilder</returns>
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

        /// <summary>
        /// Create a markdown image
        /// </summary>
        /// <param name="altText">The image alt text</param>
        /// <param name="imageUrl">The url to image</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder Image(string altText, string imageUrl)
        {
            sb.Append("!");
            Link(altText, imageUrl);

            return this;
        }

        /// <summary>
        /// Wrap text in a code block for a specific language
        /// </summary>
        /// <param name="language">The code language</param>
        /// <param name="code">The code</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder Code(string language, string code)
        {
            sb.Append("```");
            sb.AppendLine(language);
            sb.AppendLine(code);
            sb.AppendLine("```");

            return this;
        }

        /// <summary>
        /// Single Code Quote
        /// </summary>
        /// <param name="code">The code</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder CodeQuote(string code)
        {
            sb.Append("`");
            sb.Append(code);
            sb.Append("`");

            return this;
        }

        /// <summary>
        /// Create a table with the header and the given items and size
        /// </summary>
        /// <param name="headers">The header of table</param>
        /// <param name="items">The items on each row</param>
        /// <param name="diffSizeCells">True, if cells next to each other have different width.</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder Table(string[] headers, IEnumerable<string[]> items, bool diffSizeCells)
        {
            bool flag = false;
            sb.Append("| ");
            foreach (var item in headers)
            {
                if (diffSizeCells)
                {
                    if (!flag)
                    {
                        sb.Append(item + "<div><a href=\"#\"><img width=225></a></div>");
                        sb.Append(" | ");
                        flag = true;
                    }

                    else
                    {
                        sb.Append(item + "<div><a href=\"#\"><img width=525></a></div>");
                        sb.Append(" | ");
                        flag = false;
                    }
                }

                else
                {
                    sb.Append(item);
                    sb.Append(" | ");
                }
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

        /// <summary>
        /// Creates a Markdown List
        /// </summary>
        /// <param name="text">The text item for the list</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder List(string text) // nest zero
        {
            sb.Append("- ");
            sb.AppendLine(text);

            return this;
        }

        /// <summary>
        /// Create a link on a list item
        /// </summary>
        /// <param name="text">The text for the list link item</param>
        /// <param name="url">The url for link</param>
        /// <returns>The MarkdownBuilder</returns>
        public MarkdownBuilder ListLink(string text, string url) // nest zero
        {
            sb.Append("- ");
            Link(text, url);
            sb.AppendLine();

            return this;
        }

        /// <summary>
        /// Convert internal stringbuilder to string
        /// </summary>
        /// <returns>Markdown string</returns>
        public override string ToString()
        {
            return sb.ToString();
        }
    }
}
