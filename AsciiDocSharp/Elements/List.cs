namespace AsciiDocSharp.Elements
{
    public enum ListVariant
    {
        Callout,
        Ordered,
        Unordered,
    }

    public class List(
        string marker,
        ListVariant variant,
        ListItem[] items,
        string? id = null,
        BaseInline[]? title = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null
    ) : Block(id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.List;
        public string Marker = marker;
        public ListVariant Variant = variant;

        // TODO: validation that Items contains at least 1 item
        public ListItem[] Items = items;
    }
}
