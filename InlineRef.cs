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

    public class InlineRef(RefVariant variant, string target, BaseInline[] inlines, Location? location = null) : AbstractInline(ElementType.Ref, inlines, location)
    {
        public RefVariant Variant = variant;
        public string Target = target;
    }
}
