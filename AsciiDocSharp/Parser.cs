using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Threading.Tasks;

using AsciiDocSharp.Elements;

namespace AsciiDocSharp
{
    public class Parser
    {
        public class QuoteRegex(string pattern, SpanVariant variant)
        {
            public string Pattern = pattern;
            public SpanVariant Variant = variant;
        }

        public class MatchResult(int index, string value, string inner, SpanVariant variant, bool isConstrained)
        {
            public int Index = index;
            public string Value = value;
            public string Inner = inner;
            public SpanVariant Variant = variant;
            public bool IsConstrained = isConstrained;
        }

        public static readonly QuoteRegex[] ConstrainedQuoteRegexes = [
            new(@"(?<=\s|^)`\+\s*?([^`\+\00]+)\+`(?=\s|[\p{P}])", SpanVariant.LiteralMonospace),
            new(@"~\s*?([^~\00\s]+)~", SpanVariant.Subscript),
            new(@"\^\s*?([^\^\00\s]+)\^", SpanVariant.Superscript),
            new(@"(?<=\s|^)#\s*?([^#\00]+)#(?=\s|[\p{P}]|$)", SpanVariant.Mark),
            new(@"(?<=\s|^)`\s*?([^`\00]+)`(?=\s|[\p{P}]|$)", SpanVariant.Monospace),
            new(@"(?<=\s|^)\*\s*?([^\*\00]+)\*(?=\s|[\p{P}]|$)", SpanVariant.Strong),
            new(@"(?<=\s|^)_\s*?([^_\00]+)_(?=\s|[\p{P}]|$)", SpanVariant.Emphasis),
        ];

        public static readonly QuoteRegex[] UnconstrainedQuoteRegexes = [
            new(@"##\s*?([^\00(##)]+)##", SpanVariant.Mark),
            new(@"``\s*?([^\00(``)]+)``", SpanVariant.Monospace),
            new(@"\*\*\s*?([^\00(\*\*)]+)\*\*", SpanVariant.Strong),
            new(@"__\s*?([^\00(__)]+)__", SpanVariant.Emphasis)
            ];

        public static Block ParseIntoBlock(string text)
        {
            return new LeafBlock(ElementType.Paragraph);
        }

        public static BaseInline[] ParseIntoInlines(string text)
        {
            string mutText = text;

            List<MatchResult> matches = new();
            List<BaseInline> inlines = [];

            foreach (var pair in UnconstrainedQuoteRegexes)
            {
                foreach (Match match in Regex.Matches(mutText, pair.Pattern))
                {
                    matches.Add(new MatchResult(match.Index, match.Value, match.Groups.Values.Last().Value, pair.Variant, false));
                    mutText = mutText.Replace(match.Value, "\0");
                }
            }
            foreach (var pair in ConstrainedQuoteRegexes)
            {
                foreach (Match match in Regex.Matches(mutText, pair.Pattern, RegexOptions.Multiline))
                {
                    matches.Add(new MatchResult(match.Index, match.Value, match.Groups.Values.Last().Value, pair.Variant, true));
                    mutText = mutText.Replace(match.Value, "\0");
                }
            }
            string[] literalString = matches.Count > 0 ? text.Split(matches.Select(match => match.Value).ToArray(), StringSplitOptions.None) : [text];
            matches.Sort((a, b) => a.Index - b.Index);
            for (int i = 0; i < literalString.Length; i++)
            {
                if (!String.IsNullOrEmpty(literalString[i]))
                {
                    InlineLiteral literal = new(ElementType.Paragraph, literalString[i]);
                    inlines.Add(literal);
                }
                if (i < matches.Count)
                {
                    InlineSpan span = new(matches[i].Variant, matches[i].IsConstrained, ParseIntoInlines(matches[i].Inner));
                    inlines.Add(span);
                }
            }

            return [.. inlines];
        }

        public static void Parse(string input)
        {
            ParseIntoInlines(input);
            /*input = input.ReplaceLineEndings("\n").Trim();
            Document? doc = null;
            var lines = input.Split('\n');
            StringBuilder buffer = new StringBuilder();

            foreach (var pair in QuoteRegexes)
            {
                foreach (Match match in Regex.Matches(input, pair.Pattern, RegexOptions.Multiline))
                {
                    foreach (Group group in match.Groups)
                    {
                        System.Console.WriteLine(group.Value);
                    }
                }
            }

            /*foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    doc = new Document(buffer.ToString().Trim().Split('\n'));
                    buffer.Clear();
                    break;
                }
                buffer.Append(line + '\n');
            }*/
        }
    }
}
