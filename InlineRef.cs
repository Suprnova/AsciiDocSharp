using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum RefVariant
    {
        Link,
        Xref
    }

    public class InlineRef(RefVariant variant, string target) : AbstractInline
    {
        public override ElementType Name => ElementType.Ref;
        public required RefVariant Variant = variant;
        public required string Target = target;
    }
}
