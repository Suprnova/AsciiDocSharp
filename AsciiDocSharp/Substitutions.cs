using System.Text;

namespace AsciiDocSharp;

public enum SubstitutionType
{
    SpecialCharacters,
    Quotes,
    Attributes,
    Replacements,
    Macros,
    PostReplacements,
}

public enum SubstitutionGroup
{
    Header,
    None,
    Normal,
    Pass,
    Verbatim,
}

public static class Substitutions
{
    public static readonly Dictionary<string, string> SpecialCharacterDict =
        new()
        {
            { "<", "&lt;" },
            { ">", "&gt;" },
            { "&", "&amp;" },
        };

    public static readonly Dictionary<string, string> ReplacementDict =
        new()
        {
            { "(C)", "&#169" },
            { "(R)", "&#174" },
            { "(TM)", "&#8482" },
            { "...", "&#8230;&#8203" },
            { "->", "&#8594;" },
            { "=>", "&#8658" },
            { "<-", "&#8592;" },
            { "<=", "&#8656" },
            { "'", "&#8217" },
        };

    public static string Substitute(string text, SubstitutionGroup group)
    {
        SubstituteBuilder builder = new(text);
        return group switch
        {
            SubstitutionGroup.Header => builder.SpecialCharacters().Attributes().Text,
            SubstitutionGroup.None => builder.Text,
            SubstitutionGroup.Normal => builder
                                    .SpecialCharacters()
                                    .Quotes()
                                    .Attributes()
                                    .Replacements()
                                    .Macros()
                                    .PostReplacements()
                                    .Text,
            SubstitutionGroup.Pass => builder.Text,
            SubstitutionGroup.Verbatim => builder.SpecialCharacters().Text,
            _ => throw new ArgumentException("Invalid SubstitutionGroup"),
        };
    }
}

internal class SubstituteBuilder(string text)
{
    public string Text = text;

    // Status: Complete
    public SubstituteBuilder SpecialCharacters()
    {
        StringBuilder stringBuilder = new(Text);
        foreach ((string key, string value) in Substitutions.SpecialCharacterDict) {
            Text = stringBuilder.Replace(key, value).ToString();
        }
        return this;
    }

    public SubstituteBuilder Quotes() => this;

    public SubstituteBuilder Attributes() => this;

    // Status: Incomplete. Missing logic for em dash, incorrect logic for apostrophe, and
    // missing logic for replacing HTML, XML, decimal, and hexadecimal code points with the decimal code point.
    public SubstituteBuilder Replacements()
    {
        StringBuilder stringBuilder = new(Text);
        foreach ((string key, string value) in Substitutions.ReplacementDict) {
            Text = stringBuilder.Replace(key, value).ToString();
        }
        return this;
    }

    public SubstituteBuilder Macros() => this;

    public SubstituteBuilder PostReplacements() => this;
}
