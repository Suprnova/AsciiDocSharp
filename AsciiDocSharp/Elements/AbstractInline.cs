namespace AsciiDocSharp.Elements
{
    public abstract class BaseInline(ElementType context, Block? parent = null, Dictionary<string, string>? attributes = null) : AbstractNode(context, parent, attributes)
    {
        public abstract void Substitute(SubstitutionGroup subs);

        public override bool IsBlock() => false;
        public override bool IsInline() => true;
    }

    public abstract class AbstractInline(
        ElementType context,
        BaseInline[] inlines,
        Location? location = null
    ) : BaseInline(context)
    {
        public BaseInline[] Inlines = inlines;
        public Location? Location = location;

        public override void Substitute(SubstitutionGroup subs)
        {
            foreach (var inline in Inlines)
            {
                inline.Substitute(subs);
            }
        }
    }
}
