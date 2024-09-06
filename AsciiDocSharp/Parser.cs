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

        public class MatchResult(int index, string value, string inner, SpanVariant? variant = null, bool isConstrained = true)
        {
            public int Index = index;
            public string Value = value;
            public string Inner = inner;
            public SpanVariant? Variant = variant;
            public bool IsConstrained = isConstrained;
        }

        public static readonly Regex PassConstrained = new(@"(?<=\s|^)\+\s*?([^+\00]+)\+(?=\s|[\p{P}])");

        public static readonly Regex PassUnconstrained = new(@"\+\+\s*?([^(\+\+)]+)\+\+");

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

        public class InlineParser(string input)
        {
            public string Input = input;
            private string MutInput = input;
            public List<MatchResult> Matches = [];
            public List<BaseInline> inlines = [];

            public BaseInline[] Parse()
            {
                return this.Pass().UnconstrainedQuotes().ConstrainedQuotes().Finish();
            }

            public InlineParser Pass()
            {
                foreach (Match m in PassUnconstrained.Matches(MutInput))
                {
                    AddMatch(m, null, false);
                }

                foreach (Match m in PassConstrained.Matches(MutInput))
                {
                    AddMatch(m, null, true);
                }

                return this;
            }

            public InlineParser UnconstrainedQuotes()
            {
                foreach (var pair in UnconstrainedQuoteRegexes)
                {
                    foreach (Match match in Regex.Matches(MutInput, pair.Pattern))
                    {
                        AddMatch(match, pair.Variant, false);
                    }
                }

                return this;
            }

            public InlineParser ConstrainedQuotes()
            {
                foreach (var pair in ConstrainedQuoteRegexes)
                {
                    foreach (Match match in Regex.Matches(MutInput, pair.Pattern, RegexOptions.Multiline))
                    {
                        AddMatch(match, pair.Variant, true);
                    }
                }

                return this;
            }

            // TODO: This does not reassemble correctly as the index is based off of MutInput, which can change length
            public BaseInline[] Finish()
            {
                string[] outArr = Matches.Count > 0 ? Input.Split(Matches.Select(match => match.Value).ToArray(), StringSplitOptions.None) : [Input];
                Matches.Sort((a, b) => a.Index - b.Index);

                for (int i = 0; i < outArr.Length; i++)
                {
                    if (!String.IsNullOrEmpty(outArr[i]))
                    {
                        InlineLiteral literal = new(ElementType.Paragraph, outArr[i]);
                        inlines.Add(literal);
                    }
                    if (i < Matches.Count)
                    {
                        if (Matches[i].Variant is null)
                        {
                            InlineLiteral literal = new(ElementType.Pass, Matches[i].Value);
                        }
                        else
                        {
                            InlineParser innerParser = new(Matches[i].Inner);
                            InlineSpan span = new((SpanVariant)Matches[i].Variant, Matches[i].IsConstrained, innerParser.Parse());
                            inlines.Add(span);
                        }
                    }
                }
                return [.. inlines];
            }

            private void AddMatch(Match match, SpanVariant? variant, bool isConstrained)
            {
                Matches.Add(new MatchResult(match.Index, match.Value, match.Groups.Values.Last().Value, variant, isConstrained));
                MutInput = MutInput.Replace(match.Value, "\0");
            }
        }

        public static Document ParseDocument(string input)
        {
            throw new NotImplementedException();
        }
    }
}
