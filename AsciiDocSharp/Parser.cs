using System.Text.RegularExpressions;
using AsciiDocSharp.Elements;

namespace AsciiDocSharp
{
    public partial class Parser
    {
		/// <summary>
		/// Represents a relationship between a <see cref="Regex"/> pattern and its corresponding <see cref="SpanVariant"/>.
		/// </summary>
		/// <param name="pattern"><inheritdoc cref="Pattern" path="/summary"/></param>
		/// <param name="variant"><inheritdoc cref="Variant" path="/summary"/></param>
		protected class QuoteRegex(Regex pattern, SpanVariant variant)
        {
			/// <summary>
			/// The <see cref="Regex"/> pattern to match with.
			/// </summary>
			public Regex Pattern = pattern;
			/// <summary>
			/// The <see cref="SpanVariant"/> that the pattern applies to.
			/// </summary>
			public SpanVariant Variant = variant;
        }
		/// <summary>
		/// Represents a match result from a <see cref="QuoteRegex"/>.
		/// </summary>
		/// <param name="index"><inheritdoc cref="Index" path="/summary"/></param>
		/// <param name="value"><inheritdoc cref="Value" path="/summary"/></param>
		/// <param name="inner"><inheritdoc cref="Inner" path="/summary"/></param>
		/// <param name="variant"><inheritdoc cref="Variant" path="/summary"/></param>
		/// <param name="isConstrained"><inheritdoc cref="IsConstrained" path="/summary"/></param>
		protected class MatchResult(
            int index,
            string value,
            string inner,
            SpanVariant? variant = null,
            bool isConstrained = true
        )
        {
			/// <summary>
			/// The position of the first character of the match.
			/// </summary>
			public int Index = index;
			/// <summary>
			/// The matching value, including the formatting marks.
			/// </summary>
			public string Value = value;
			/// <summary>
			/// The matching value, excluding the formatting marks.
			/// </summary>
			public string Inner = inner;
			/// <summary>
			/// The <see cref="SpanVariant"/> that the result represents.
			/// </summary>
			public SpanVariant? Variant = variant;
			/// <summary>
			/// Whether or not the result is constrained.
			/// </summary>
			public bool IsConstrained = isConstrained;
        }

        /// <summary>
        /// Matches any constrained <see langword="string"/> surrounded by <c>+</c>s.
        /// </summary>
        protected static readonly Regex PassConstrained = RxPassthroughConstrained();

		/// <summary>
		/// Matches any <see langword="string"/> surrounded by <c>++</c>s.
		/// </summary>
		protected static readonly Regex PassUnconstrained = RxPassthrough();

		/// <summary>
		/// Represents a collection of <see cref="QuoteRegex"/> patterns that are constrained.
		/// </summary>
		/// <remarks>
		/// Matches the following formatting marks and their corresponding <see cref="SpanVariant"/>s in the following order:
		/// <list type="bullet">
		/// <item>
		/// <term>`+ +`</term>
		/// <description><see cref="SpanVariant.LiteralMonospace"/></description>
		/// </item>
		/// <item>
		/// <term># #</term>
		/// <description><see cref="SpanVariant.Mark"/></description>
		/// </item>
		/// <item>
		/// <term>` `</term>
		/// <description><see cref="SpanVariant.Monospace"/></description>
		/// </item>
		/// <item>
		/// <term>* *</term>
		/// <description><see cref="SpanVariant.Strong"/></description>
		/// </item>
		/// <item>
		/// <term>_ _</term>
		/// <description><see cref="SpanVariant.Emphasis"/></description>
        /// </item>
        /// </list>
        /// </remarks>
		protected static readonly QuoteRegex[] ConstrainedQuoteRegexes =
        [
            new(RxLiteralMonoSpaceConstrained(), SpanVariant.LiteralMonospace),
            new(RxMarkConstrained(), SpanVariant.Mark),
            new(RxMonospaceConstrained(), SpanVariant.Monospace),
            new(RxStrongConstrained(), SpanVariant.Strong),
            new(RxEmphasisConstrained(), SpanVariant.Emphasis),
        ];

		/// <summary>
		/// Represents a collection of <see cref="QuoteRegex"/> patterns that are unconstrained.
		/// </summary>
		/// <remarks>
		/// Matches the following formatting marks and their corresponding <see cref="SpanVariant"/>s in the following order:
		/// <list type="bullet">
		/// <item>
		/// <term>~ ~</term>
		/// <description><see cref="SpanVariant.Subscript"/></description>
		/// </item>
		/// <item>
		/// <term>^ ^</term>
		/// <description><see cref="SpanVariant.Superscript"/></description>
		/// </item>
		/// <item>
		/// <term>## ##</term>
		/// <description><see cref="SpanVariant.Mark"/></description>
		/// </item>
		/// <item>
		/// <term>`` ``</term>
		/// <description><see cref="SpanVariant.Monospace"/></description>
		/// </item>
		/// <item>
		/// <term>** **</term>
		/// <description><see cref="SpanVariant.Strong"/></description>
		/// </item>
		/// <item>
		/// <term>__ __</term>
		/// <description><see cref="SpanVariant.Emphasis"/></description>
		/// </item>
		/// </list>
		/// </remarks>
		protected static readonly QuoteRegex[] UnconstrainedQuoteRegexes =
		[
			new(RxSubscript(), SpanVariant.Subscript),
			new(RxSuperscript(), SpanVariant.Superscript),
			new(RxMark(), SpanVariant.Mark),
            new(RxMonospace(), SpanVariant.Monospace),
            new(RxStrong(), SpanVariant.Strong),
            new(RxEmphasis(), SpanVariant.Emphasis),
        ];

		/// <summary>
		/// Represents a parser for inline elements.
		/// </summary>
		/// <remarks>
		/// This class is to be instantiated with a string to parse, and then the <c>Parse</c> method is called to return the parsed inlines.
		/// Inline parsing involves apply text formatting to each inline element. Formatting marks come in two forms, <c>constrained</c> and <c>unconstrained</c>.
		/// Constrained formatting marks are typically single formatting pairs <c>*like* _this_</c>, and surround words or phrases. Constrained formatting marks
		/// must be preceded by whitespace, and followed by whitespace or punctuation. Unconstrained formatting marks do not have these restrictions, and are typically
		/// a more verbose version of their constrained counterpart ,<c>**like**__this__</c>. Unconstrained formatting takes precedence over constrained formatting, but
		/// both are superseded by passthrough formatting, typically used to escape formatting marks.
		/// <para>
		/// For more information on text formatting, see the <see href="https://docs.asciidoctor.org/asciidoc/latest/text/">documentation on text formatting</see>.
		/// </para>
		/// </remarks>
		/// <param name="input">The text to parse.</param>
		public class InlineParser(string input)
        {
			/// <summary>
			/// An immutable representation of the input text.
			/// </summary>
			private readonly string Input = input;
			/// <summary>
			/// A mutable representation of the input text. When matches are found, they are replaced with null characters to avoid re-matching.
			/// </summary>
			private string MutInput = input;
			/// <summary>
			/// A collection of matches found during parsing.
			/// </summary>
			private readonly List<MatchResult> Matches = [];
			/// <summary>
			/// A collection of the parsed inlines to be returned after parsing.
			/// </summary>
			private readonly List<BaseInline> inlines = [];

			/// <summary>
			/// Parses the input text and returns the formatted inlines.
			/// </summary>
			/// <returns>An array of the formatted inlines.</returns>
			public BaseInline[] Parse()
            {
                return this.Pass().UnconstrainedQuotes().ConstrainedQuotes().Finish();
            }

			/// <summary>
			/// Applies passthrough formatting to the input text.
			/// </summary>
			/// <returns><see langword="this"/></returns>
			private InlineParser Pass()
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

			/// <summary>
			/// Applies unconstrained formatting to the input text.
			/// </summary>
			/// <returns><see langword="this"/></returns>
			private InlineParser UnconstrainedQuotes()
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

			/// <summary>
			/// Applies constrained formatting to the input text.
			/// </summary>
			/// <returns><see langword="this"/></returns>
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

			// TODO: review why SpanVariant is nullable and why we have varying fallbacks
			// TODO: ensure there's logic to handle what kind of inlines can be nested in other inlines
			/// <summary>
			/// Constructs and returns a collection of the formatted inlines.
			/// </summary>
			/// <returns>An array of the formatted inlines.</returns>
			private BaseInline[] Finish()
            {
				// Split the input text by the matches to preserve the original text
				// and sort the matches by index to maintain original order
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
						// If string isn't empty, then we didn't find a match in formatting.
						// We can assume it's a literal string and add it to the inlines.
						InlineLiteral literal = new(ElementType.Paragraph, outArr[i]);
                        inlines.Add(literal);
                    }
                    if (i < Matches.Count)
                    {
                        if (Matches[i].Variant is null)
                        {
							// If it was matched but had a null variant, we can assume it's a passthrough
							InlineLiteral literal = new(ElementType.Pass, Matches[i].Value);
                        }
                        else
                        {
							// Do another pass on the inner text to parse any nested inlines
							InlineParser innerParser = new(Matches[i].Inner);
							InlineSpan span =
                                new(
									// Fallback to a mark if the variant is null
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

			/// <summary>
			/// Parses a <see cref="Match"/> into a <see cref="MatchResult"/>, adds it to the <see cref="Matches"/> collection,
			/// and replaces the match in the <see cref="MutInput"/>.
			/// </summary>
			/// <param name="match">The match to be added.</param>
			/// <param name="variant">The type of <see cref="SpanVariant"/> of the match.</param>
			/// <param name="isConstrained">Whether or not the match is constrained.</param>
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
                // We want to maintain the same length to avoid index issues, and \0 is unparsable
                // in the Regex to avoid matching multiple times
                MutInput = MutInput.Replace(
                    match.Value,
                    String.Concat(Enumerable.Repeat("\0", match.Value.Length))
                );
            }
        }


        [GeneratedRegex(@"\+\+\s*?([^(\+\+)]+)\+\+")]
        private static partial Regex RxPassthrough();

        [GeneratedRegex(@"(?<=\s|^)\+\s*?([^+\00]+)\+(?=\s|[\p{P}])")]
        private static partial Regex RxPassthroughConstrained();

        [GeneratedRegex(@"(?<=\s|^)`\+\s*?([^`\+\00]+)\+`(?=\s|[\p{P}])")]
        private static partial Regex RxLiteralMonoSpaceConstrained();

        [GeneratedRegex(@"~\s*?([^~\00\s]+)~")]
        private static partial Regex RxSubscript();

        [GeneratedRegex(@"\^\s*?([^\^\00\s]+)\^")]
        private static partial Regex RxSuperscript();

        [GeneratedRegex(@"(?<=\s|^)#\s*?([^#\00]+)#(?=\s|[\p{P}]|$)")]
        private static partial Regex RxMarkConstrained();

        [GeneratedRegex(@"(?<=\s|^)`\s*?([^`\00]+)`(?=\s|[\p{P}]|$)")]
        private static partial Regex RxMonospaceConstrained();

        [GeneratedRegex(@"(?<=\s|^)\*\s*?([^\*\00]+)\*(?=\s|[\p{P}]|$)")]
        private static partial Regex RxStrongConstrained();

        [GeneratedRegex(@"(?<=\s|^)_\s*?([^_\00]+)_(?=\s|[\p{P}]|$)")]
        private static partial Regex RxEmphasisConstrained();

        [GeneratedRegex(@"##\s*?([^\00(##)]+)##")]
        private static partial Regex RxMark();

        [GeneratedRegex(@"``\s*?([^\00(``)]+)``")]
        private static partial Regex RxMonospace();

        [GeneratedRegex(@"\*\*\s*?([^\00(\*\*)]+)\*\*")]
        private static partial Regex RxStrong();

        [GeneratedRegex(@"__\s*?([^\00(__)]+)__")]
        private static partial Regex RxEmphasis();
    }
}
