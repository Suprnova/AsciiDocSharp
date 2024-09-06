namespace AsciiDocSharp.Elements
{
    public class ListItem(
        string marker,
        BaseInline[] principal,
        Block[]? blocks = null,
        string? id = null,
        BaseInline[]? title = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null
    ) : AbstractListItem(marker, blocks, principal, id, title, refText, metadata, location)
    {
        public const ElementType Name = ElementType.ListItem;
    }

    // TODO: Add a clause to the constructor that ensures principal is populated.
}
