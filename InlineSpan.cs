using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public enum SpanVariant
    {
        Strong,
        Emphasis,
        Code,
        Mark
    }

    public class InlineSpan(SpanVariant variant, bool isConstrained) : AbstractInline
    {
        public override InlineName Name { get { return InlineName.Span; } }
        public required SpanVariant Variant = variant;
        public required bool IsConstrained = isConstrained;
    }
}
