namespace AsciiDocSharp.Elements
{
    public enum ContentModel
    {
        Compound,
        Empty,
        Raw,
        Simple,
        Verbatim,
    }

    public abstract class Block(
            ElementType context,
            BaseInline? title = null,
            BaseInline? refText = null,
            Block? parent = null,
            Dictionary<string, string>? attributes = null) : AbstractNode(context, parent, attributes)
    {
        public ContentModel ContentModel { get; set; }
        public BaseInline? Title { get; set; } = title;
        public BaseInline? RefText { get; set; } = refText;

        public override bool IsBlock() => true;
        public override bool IsInline() => false;

        // temporary fix to allow compiling
        public override string GenerateId() => "";
    }
}
