﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public abstract class BaseInline(ElementType name)
    {
        public required ElementType Name { get; set; } = name;
    }

    public abstract class AbstractInline(ElementType name, BaseInline[] inlines, Location? location) : BaseInline(name)
    {
        public const PositionType Type = PositionType.Inline;
        public required BaseInline[] Inlines = inlines;
        public Location? Location = location;
    }
}
