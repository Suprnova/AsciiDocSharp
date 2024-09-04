using AsciiDocSharp.Elements;

class Program
{
    static void Main(string[] args)
    {
        InlineLiteral line = new(ElementType.Text, "body", new Location(3, 1, 3, 4));
        LeafBlock paragraph = new(ElementType.Paragraph, inlines: [line], location: new Location(3, 1, 3, 4));
        InlineLiteral title = new(ElementType.Text, "Document Title", new Location(1, 3, 1, 16));
        Header header = new Header([title], location: new Location(1, 1, 1, 16));
        Document document = new(header: header, blocks: [paragraph], location: new Location(1, 1, 1, 16));
    }
}