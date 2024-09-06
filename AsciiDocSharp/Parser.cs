using System.Text.RegularExpressions;
using AsciiDocSharp.Elements;

namespace AsciiDocSharp
{
    public partial class Parser
    {
        public class QuoteRegex(Regex pattern, SpanVariant variant)
        {
            public Regex Pattern = pattern;
            public SpanVariant Variant = variant;
        }

        public class MatchResult(
            int index,
            string value,
            string inner,
            SpanVariant? variant = null,
            bool isConstrained = true
        )
        {
            public int Index = index;
            public string Value = value;
            public string Inner = inner;
            public SpanVariant? Variant = variant;
            public bool IsConstrained = isConstrained;
        }

        public static readonly Regex PassConstrained = RxPassCon();

        public static readonly Regex PassUnconstrained = RxPass();

        public static readonly QuoteRegex[] ConstrainedQuoteRegexes =
        [
            new(RxLitMonoCon(), SpanVariant.LiteralMonospace),
            new(RxSubCon(), SpanVariant.Subscript),
            new(RxSupCon(), SpanVariant.Superscript),
            new(RxMarkCon(), SpanVariant.Mark),
            new(RxMonoCon(), SpanVariant.Monospace),
            new(RxStrongCon(), SpanVariant.Strong),
            new(RxEmCon(), SpanVariant.Emphasis),
        ];

        public static readonly QuoteRegex[] UnconstrainedQuoteRegexes =
        [
            new(RxMark(), SpanVariant.Mark),
            new(RxMono(), SpanVariant.Monospace),
            new(RxStrong(), SpanVariant.Strong),
            new(RxEm(), SpanVariant.Emphasis),
        ];

        public static Block ParseIntoBlock()
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
                    foreach (Match match in pair.Pattern.Matches(MutInput))
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
                    foreach (Match match in pair.Pattern.Matches(MutInput))
                    {
                        AddMatch(match, pair.Variant, true);
                    }
                }

                return this;
            }

            // TODO: This does not reassemble correctly as the index is based off of MutInput, which can change length
            public BaseInline[] Finish()
            {
                string[] outArr =
                    Matches.Count > 0
                        ? Input.Split(
                            Matches.Select(match => match.Value).ToArray(),
                            StringSplitOptions.None
                        )
                        : [Input];
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
                            InlineSpan span =
                                new(
                                    Matches[i].Variant ?? SpanVariant.Mark,
                                    Matches[i].IsConstrained,
                                    innerParser.Parse()
                                );
                            inlines.Add(span);
                        }
                    }
                }
                return [.. inlines];
            }

            private void AddMatch(Match match, SpanVariant? variant, bool isConstrained)
            {
                Matches.Add(
                    new MatchResult(
                        match.Index,
                        match.Value,
                        match.Groups.Values.Last().Value,
                        variant,
                        isConstrained
                    )
                );
                MutInput = MutInput.Replace(match.Value, "\0");
            }
        }

        public static Document ParseDocument(string input)
        {
            throw new NotImplementedException();
        }

        [GeneratedRegex(@"\+\+\s*?([^(\+\+)]+)\+\+")]
        private static partial Regex RxPass();

        [GeneratedRegex(@"(?<=\s|^)\+\s*?([^+\00]+)\+(?=\s|[\p{P}])")]
        private static partial Regex RxPassCon();

        [GeneratedRegex(@"(?<=\s|^)`\+\s*?([^`\+\00]+)\+`(?=\s|[\p{P}])")]
        private static partial Regex RxLitMonoCon();

        [GeneratedRegex(@"~\s*?([^~\00\s]+)~")]
        private static partial Regex RxSubCon();

        [GeneratedRegex(@"\^\s*?([^\^\00\s]+)\^")]
        private static partial Regex RxSupCon();

        [GeneratedRegex(@"(?<=\s|^)#\s*?([^#\00]+)#(?=\s|[\p{P}]|$)")]
        private static partial Regex RxMarkCon();

        [GeneratedRegex(@"(?<=\s|^)`\s*?([^`\00]+)`(?=\s|[\p{P}]|$)")]
        private static partial Regex RxMonoCon();

        [GeneratedRegex(@"(?<=\s|^)\*\s*?([^\*\00]+)\*(?=\s|[\p{P}]|$)")]
        private static partial Regex RxStrongCon();

        [GeneratedRegex(@"(?<=\s|^)_\s*?([^_\00]+)_(?=\s|[\p{P}]|$)")]
        private static partial Regex RxEmCon();

        [GeneratedRegex(@"##\s*?([^\00(##)]+)##")]
        private static partial Regex RxMark();

        [GeneratedRegex(@"``\s*?([^\00(``)]+)``")]
        private static partial Regex RxMono();

        [GeneratedRegex(@"\*\*\s*?([^\00(\*\*)]+)\*\*")]
        private static partial Regex RxStrong();

        [GeneratedRegex(@"__\s*?([^\00(__)]+)__")]
        private static partial Regex RxEm();
    }
}
