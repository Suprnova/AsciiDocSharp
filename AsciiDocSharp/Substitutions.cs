using AsciiDocSharp.Elements;
using System.Text;

namespace AsciiDocSharp
{
    public enum SubstitutionType
    {
        SpecialCharacters,
        Quotes,
        Attributes,
        Replacements,
        Macros,
        PostReplacements
    }

    public enum SubstitutionGroup
    {
        Header,
        None,
        Normal,
        Pass,
        Verbatim
    }

    public class SubstitutionPair(string openingMark, string closingMark, SpanVariant variant)
    {
        public string OpeningMark = openingMark;
        public string ClosingMark = closingMark;
        public SpanVariant Variant = variant;
    }

    public static class Substitutions
    {
        public static readonly SubstitutionPair[] QuoteDict = [
            new("*", "*", SpanVariant.Strong),
            new("_", "_", SpanVariant.Emphasis),
            new("`", "`", SpanVariant.Monospace),
            new("`+", "+`", SpanVariant.LiteralMonospace),
            new("#", "#", SpanVariant.Mark),
            new("~", "~", SpanVariant.Subscript),
            new("^", "^", SpanVariant.Superscript)
            ];

        public static readonly Dictionary<string, string> SpecialCharacterDict = new()
        {
            { "<", "&lt;" },
            { ">", "&gt;" },
            { "&", "&amp;" }
        };

        public static readonly Dictionary<string, string> ReplacementDict = new()
        {
            { "(C)", "&#169" },
            { "(R)", "&#174" },
            { "(TM)", "&#8482" },
            { "...", "&#8230;&#8203" },
            { "->", "&#8594;" },
            { "=>", "&#8658" },
            { "<-", "&#8592;" },
            { "<=", "&#8656" },
            { "'", "&#8217" }
        };

        public static string Substitute(string text, SubstitutionGroup group)
        {
            SubstituteBuilder builder = new(text);
            switch (group)
            {
                case SubstitutionGroup.Header:
                    return builder
                        .SpecialCharacters()
                        .Attributes()
                        .Text;
                case SubstitutionGroup.None:
                    return builder.Text;
                case SubstitutionGroup.Normal:
                    return builder
                        .SpecialCharacters()
                        .Quotes()
                        .Attributes()
                        .Replacements()
                        .Macros()
                        .PostReplacements()
                        .Text;
                case SubstitutionGroup.Pass:
                    return builder.Text;
                case SubstitutionGroup.Verbatim:
                    return builder
                        .SpecialCharacters()
                        .Text;
            }
            throw new ArgumentException("Invalid SubstitutionGroup");
        }
    }

    class SubstituteBuilder(string text)
    {
        public string Text = text;

        // Status: Complete
        public SubstituteBuilder SpecialCharacters()
        {
            StringBuilder stringBuilder = new(Text);
            foreach (var (key, value) in Substitutions.SpecialCharacterDict)
            {
                Text = stringBuilder.Replace(key, value).ToString();
            }
            return this;
        }

        public SubstituteBuilder Quotes()
        {
            return this;
        }

        public SubstituteBuilder Attributes()
        {
            return this;
        }

        // Status: Incomplete. Missing logic for em dash, incorrect logic for apostrophe, and
        // missing logic for replacing HTML, XML, decimal, and hexadecimal code points with the decimal code point.
        public SubstituteBuilder Replacements()
        {
            StringBuilder stringBuilder = new(Text);
            foreach (var (key, value) in Substitutions.ReplacementDict)
            {
                Text = stringBuilder.Replace(key, value).ToString();
            }
            return this;
        }

        public SubstituteBuilder Macros()
        {
            return this;
        }

        public SubstituteBuilder PostReplacements()
        {
            return this;
        }
    }
}
