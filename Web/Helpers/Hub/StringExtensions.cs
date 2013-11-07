namespace Telerik.Web.Mvc.Examples
{
    using System;
    using System.Text;
    using System.Text.RegularExpressions;

    public static class StringExtensions
    {
        private static string TrimHtmlWhiteSpace(this string html)
        {
            return new Regex("(\\s|\t|\n|\r)+").Replace(html, " ");
        }


        private static int indentSize = 4, indentLevel = 0;

        private static string newLine = Environment.NewLine;
        private static int newLineLength = newLine.Length;

        private static string blockElements = "div|p|img|ul|ol|li|h[1-6]|blockquote";
        private static Regex openingBlockRegex = new Regex("<(" + blockElements + ")[^>]*>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
              closingBlockRegex = new Regex("</(" + blockElements + ")>", RegexOptions.IgnoreCase | RegexOptions.Compiled),
              emptyTagRegex = new Regex("^<.*/>$", RegexOptions.Compiled),
              brTagRegex = new Regex("^<br\\s*/>$", RegexOptions.Compiled);

        private const int WRAP_THRESHOLD = 120;

        public static string IndentTags(this string html)
        {
            bool indenting = true;

            html = new Regex("(<[\\w][\\w\\d]*[^>]*>)|(</[\\w][\\w\\d]*>)", RegexOptions.IgnoreCase).Replace(html, new MatchEvaluator(delegate(Match m)
            {
                var token = m.Groups[0].Value;
                var result = new StringBuilder();
                var currentIndent = newLine.PadRight(newLineLength + indentSize * indentLevel);

                if (openingBlockRegex.IsMatch(token))
                {
                    if (!indenting)
                        result.Append(currentIndent);

                    if (!emptyTagRegex.IsMatch(token))
                        ++indentLevel;

                    while (token.Length > WRAP_THRESHOLD)
                    {
                        int splitPoint = token.Substring(0, WRAP_THRESHOLD).LastIndexOf(' ');

                        result.Append(token.Substring(0, splitPoint))
                              .Append(newLine.PadRight(newLineLength + indentSize * (indentLevel + 1)));

                        token = token.Substring(splitPoint);
                    }

                    result.Append(token)
                          .Append(newLine.PadRight(newLineLength + indentSize * indentLevel));

                    indenting = true;
                }
                else if (closingBlockRegex.IsMatch(token))
                {
                    result.Append(newLine.PadRight(newLineLength + indentSize * (--indentLevel)))
                          .Append(token);
                    indenting = false;
                }
                else if (brTagRegex.IsMatch(token))
                {
                    result.Append(token)
                          .Append(newLine.PadRight(newLineLength + indentSize * indentLevel));
                    indenting = false;
                }
                else
                {
                    result.Append(token);
                    indenting = false;
                }

                return result.ToString();
            }));

            return html;
        }

        public static string WordWrap(this string html)
        {
            // wrap lines
            var lines = html.Split(new string[] { newLine }, StringSplitOptions.RemoveEmptyEntries);

            for (var i = 0; i < lines.Length; i++)
            {
                if (lines[i].Length <= WRAP_THRESHOLD)
                    continue;

                var result = new StringBuilder();

                var currentLine = lines[i];

                var currentLineIndentSize = new Regex("^\\s+").Match(currentLine).Length;

                while (currentLine.Length > WRAP_THRESHOLD)
                {
                    int splitPoint = currentLine.Substring(0, WRAP_THRESHOLD - indentSize).LastIndexOf(' ');

                    if (splitPoint < 0)
                        splitPoint = WRAP_THRESHOLD; // cuts though code, though

                    result.Append(currentLine.Substring(0, splitPoint))
                          .Append(newLine.PadRight(newLineLength + currentLineIndentSize + indentSize));

                    currentLine = currentLine.Substring(splitPoint + 1);
                }

                result.Append(currentLine);

                lines[i] = result.ToString();
            }

            return String.Join(newLine, lines);
        }

        public static string IndentHtml(this string html)
        {
            return html.TrimHtmlWhiteSpace()
                       .IndentTags()
                       .WordWrap();
        }

#if MVC1 || MVC2
        public static string Raw(this string value)
        {
            return value;
        }
#else
        public static System.Web.IHtmlString Raw(this string value)
        {
            return new System.Web.HtmlString(value);
        }
#endif
    }
}