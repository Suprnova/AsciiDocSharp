namespace AsciiDocSharp.Elements
{
    public enum PositionType
    {
        Block,
        Inline,
    }

    public abstract class AbstractBlock(
        string? id = null,
        BaseInline[]? title = null,
        BaseInline[]? refText = null,
        BlockMetadata? metadata = null,
        Location? location = null,
        Document? document = null
    )
    {
        public const PositionType Type = PositionType.Block;

        public string? Id { get; set; } = id;
        public BaseInline[]? Title { get; set; } = title;
        public BaseInline[]? RefText { get; set; } = refText;
        public BlockMetadata? Metadata { get; set; } = metadata;
        public Location? Location { get; set; } = location;

        // Temp fix to compile
        public Document? Document { get; set; } = document;
	}

    public class BlockMetadata(
        Dictionary<string, string>? attributes = null,
        string[]? options = null,
        string[]? roles = null,
        Location? location = null
    )
    {
        public Dictionary<string, string> Attributes = attributes ?? [];
        public string[] Options = options ?? [];
        public string[] Roles = roles ?? [];

        public Location? Location = location;
    }
}
