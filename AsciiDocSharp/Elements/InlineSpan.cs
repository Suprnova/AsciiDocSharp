namespace AsciiDocSharp.Elements
{
    public enum SpanVariant
    {
        Strong,
        Emphasis,
        Monospace,
        LiteralMonospace,
        Superscript,
        Subscript,
        Mark,
    }

    public class InlineSpan(
        SpanVariant variant,
        bool isConstrained,
        BaseInline[] inlines,
        Location? location = null
    ) : AbstractInline(ElementType.Span, inlines, location)
    {
        public SpanVariant Variant = variant;
        public bool IsConstrained = isConstrained;
    }
}
