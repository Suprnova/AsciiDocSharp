using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum ElementType
    {
        Block,
        Inline
    }

    public abstract class AbstractBlock
    {
        public required ElementType Type { get; set; }

        public string? Id { get; set; }
        public BaseInline[]? Title { get; set; }
        public BaseInline[]? RefText { get; set; }
        public BlockMetadata? Metadata { get; set; }
        public Location? Location { get; set; }
    }

    public class BlockMetadata
    {
        public required Dictionary<String, String> Attributes { get; set; } = [];
        public required String[] Options { get; set; } = [];
        public required String[] Roles { get; set; } = [];

        public Location? Location { get; set; }
    }
}
