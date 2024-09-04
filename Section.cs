﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Section(int level, BaseInline[] title, Block[]? blocks = null, string? id = null, BaseInline[]? refText = null, BlockMetadata? metadata = null, Location? location = null) : AbstractHeading(level, title, id, refText, metadata, location)
    {
        public const ElementType Name = ElementType.Section;

        public Block[] Blocks { get; set; } = blocks ?? ([]);
    }
}
