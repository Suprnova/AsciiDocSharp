using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public abstract class BaseInline
    {
        public abstract ElementType Name { get; }
    }

    public abstract class AbstractInline : BaseInline
    {
        public const string Type = "inline";
        public required BaseInline[] Inlines;
        public Location? Location;
    }
}
