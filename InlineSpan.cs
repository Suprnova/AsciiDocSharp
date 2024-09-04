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

    public class InlineSpan(SpanVariant variant, bool isConstrained, BaseInline[] inlines, Location? location) : AbstractInline(ElementType.Span, inlines, location)
    {
        public required SpanVariant Variant = variant;
        public required bool IsConstrained = isConstrained;
    }
}
