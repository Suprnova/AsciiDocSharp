using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum PositionType
    {
        Block,
        Inline
    }

    public abstract class AbstractBlock(string? id, BaseInline[]? title, BaseInline[]? refText, BlockMetadata? metadata, Location? location)
    {
        public const PositionType Type = PositionType.Block;

        public string? Id { get; set; } = id;
        public BaseInline[]? Title { get; set; } = title;
        public BaseInline[]? RefText { get; set; } = refText;
        public BlockMetadata? Metadata { get; set; } = metadata;
        public Location? Location { get; set; } = location;
    }

    public class BlockMetadata(Dictionary<String, String>? attributes, String[]? options, String[]? roles, Location? location)
    {
        public required Dictionary<String, String> Attributes = attributes ?? [];
        public required String[] Options  = options ?? [];
        public required String[] Roles = roles ?? [];

        public Location? Location = location;
    }
}
