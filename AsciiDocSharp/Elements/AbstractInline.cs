using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public abstract class BaseInline(ElementType name)
    {
        public ElementType Name { get; set; } = name;

        public abstract void Substitute(SubstitutionGroup subs);
    }

    public abstract class AbstractInline(ElementType name, BaseInline[] inlines, Location? location = null) : BaseInline(name)
    {
        public const PositionType Type = PositionType.Inline;
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
