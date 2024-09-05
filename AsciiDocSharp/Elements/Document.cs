using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public class Document
    {
        public const ElementType Name = ElementType.Document;
        public const PositionType Type = PositionType.Block;

        public Dictionary<string, string> Attributes = [];
        public Header? DocHeader;
        public Block[]? Blocks = [];
        public Location? Location;

        // TODO: probably should move all of this logic to the parser class
        public Document(IEnumerable<string> strings)
        {
            for (int i = 0; i < strings.Count(); i++)
            {
                string s = strings.ElementAt(i);
                if (s.StartsWith("//")) continue;
                else if (s.StartsWith('='))
                {
                    DocHeader ??= new Header();

                    if (DocHeader.Title.Length != 0)
                    {
                        // TODO: log error, dont throw exception, and add handling for doctype
                        throw new Exception("level 0 sections can only be used when doctype is book");
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
                    string value = s[(s.IndexOf(':', 1)+1)..].Trim();
                    Attributes ??= [];
                    Attributes.Add(key, value);
                }
                else
                {
                    // This can't be a valid header, so we stop parsing and return what we have
                    if (DocHeader?.Title is null) {
                        return;
                    }
                    Author author = new(s.Trim());
                    DocHeader.Authors = [.. DocHeader.Authors, author];
                }
            }
            return;

        }
    }

    public class Header(BaseInline[]? title = null, Author[]? authors = null, Location? location = null)
    {
        public BaseInline[] Title = title ?? [];
        public Author[] Authors = authors ?? [];
        public Location? Location = location;
    }
}
