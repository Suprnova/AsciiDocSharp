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

    public abstract class AbstractBlock(string? id = null, BaseInline[]? title = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null)
    {
        public const PositionType Type = PositionType.Block;

        public string? Id { get; set; } = id;
        public BaseInline[]? Title { get; set; } = title;
        public BaseInline[]? RefText { get; set; } = refText;
        public BlockMetadata? Metadata { get; set; } = metadata;
        public Location? Location { get; set; } = location;
    }

    public class BlockMetadata(Dictionary<String, String>? attributes = null, String[]? options = null, String[]? roles = null, Location? location = null)
    {
        public Dictionary<String, String> Attributes = attributes ?? [];
        public String[] Options  = options ?? [];
        public String[] Roles = roles ?? [];

        public Location? Location = location;
    }
}
