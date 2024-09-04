using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class InlineLiteral : BaseInline
    {
#pragma warning disable IDE1006 // Naming Styles
        private ElementType name
#pragma warning restore IDE1006 // Naming Styles
        {
            get { return name; }
            set
            {
                if (name == ElementType.Ref || name == ElementType.Span)
                {
                    throw new InvalidOperationException();
                }
            }
        }
        public override ElementType Name => name;
        public const string Type = "string";
        public required string Value;

        public Location? Location;

        public InlineLiteral(ElementType name, string value, Location? location)
        {
            this.name = name;
            Value = value;
            Location = location;
        }

    }
}
