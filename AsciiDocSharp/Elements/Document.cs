namespace AsciiDocSharp.Elements;

public class Document : Block
{
    public string? BaseDir { get; private set; }
    public Block[] Blocks { get; set; } = [];
    public bool CompatibilityMode { get; init; }
    public string? OutfileSuffix { get; init; }
    public Document? ParentDocument { get; private set; }
    public SafeMode Safe { get; init; }
    public bool SourceMap {
        get => GetAttribute("sourcemap", "false") == "true";
        set => SetAttribute("sourcemap", value ? "true" : "false");
    }
    public Configuration Options { get; init; }

    public NotImplementedException? Catalog { get; init; }
    public NotImplementedException? Backend { get; init; }
    public NotImplementedException? Counters { get; init; }
    public NotImplementedException? DocType { get; init; }
    public Header? DocHeader { get; init; }
    public NotImplementedException? Reader { get; init; }
    public NotImplementedException? PathResolver { get; init; }
    public NotImplementedException? Converter { get; init; }
    public NotImplementedException? SyntaxHighlighter { get; init; }
    public NotImplementedException? Extensions { get; init; }

    // TODO: send title, refText, and attributes to base constructor
    public Document(Configuration options, string? data = null) : base(ElementType.Document)
    {
        Options = options;

        if (options.BaseDir is not null) {
            BaseDir = options.BaseDir;
        } else if (Attributes.GetAttribute("docdir") is not null) {
            BaseDir = Attributes.GetAttribute("docdir");
        } else {
            // TODO: change to working directory
            BaseDir = ".";
            _ = Attributes.SetAttribute("docdir", BaseDir);
        }

        CompatibilityMode = Attributes.GetAttribute("compat-mode") == "true";
        if (data is not null) {
            // TODO: parse data
        }
    }

    // Temporary constructor until parsing is implemented
    public Document(BaseInline title, Block[] blocks, Configuration options) : base(ElementType.Document, title)
    {
        DocHeader = new Header(title);
        Options = options;
        Blocks = blocks;
    }

    [System.Diagnostics.CodeAnalysis.SuppressMessage("CodeQuality", "IDE0051:Remove unused private members", Justification = "WIP Class")]
    private void SetParent(Document other)
    {
        ParentDocument = other;
        BaseDir = other.BaseDir;
    }

    // TODO: probably should move all of this logic to the parser class
    /*public Document(IEnumerable<string> strings)
    {
        for (int i = 0; i < strings.Count(); i++)
        {
            string s = strings.ElementAt(i);
            if (s.StartsWith("//"))
                continue;
            else if (s.StartsWith('='))
            {
                DocHeader ??= new Header();

                if (DocHeader.Title.Length != 0)
                {
                    // TODO: log error, dont throw exception, and add handling for doctype
                    throw new Exception(
                        "level 0 sections can only be used when doctype is book"
                    );
                }
                else
                {
                    InlineLiteral title = new(ElementType.Text, s.Substring(1).Trim());
                    DocHeader.Title = [title];
                }
            }
            else if (s.StartsWith(':'))
            {
                string key = s[1..s.IndexOf(':', 1)].Trim();
                string value = s[(s.IndexOf(':', 1) + 1)..].Trim();
                Attributes ??= [];
                Attributes.Add(key, value);
            }
            else
            {
                // This can't be a valid header, so we stop parsing and return what we have
                if (DocHeader?.Title is null)
                {
                    return;
                }
                Author author = new(s.Trim());
                DocHeader.Authors = [.. DocHeader.Authors, author];
            }
        }
        return;
    }
}*/

    public class Header(
    BaseInline? title = null,
    Author[]? authors = null,
    Location? location = null
)
    {
        public BaseInline? Title = title;
        public Author[] Authors = authors ?? [];
        public Location? Location = location;
    }
}
