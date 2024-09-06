namespace AsciiDocSharp.Elements
{
    public abstract class Block(
        string? id = null,
        BaseInline[]? title = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null
    ) : AbstractBlock(id, title, refText, metadata, location) { }
}
