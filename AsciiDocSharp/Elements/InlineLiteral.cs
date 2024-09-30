namespace AsciiDocSharp.Elements
{
    public class InlineLiteral(ElementType name, string value, Location? location = null)
        : BaseInline(name)
    {
        public const string Type = "string";
        public string Value = value;

        public Location? Location = location;

        public override void Substitute(SubstitutionGroup subs)
        {
            Substitutions.Substitute(Value, subs);
        }

        // temporary fix to allow compiling
        public override string GenerateId() => "";
    }
}
