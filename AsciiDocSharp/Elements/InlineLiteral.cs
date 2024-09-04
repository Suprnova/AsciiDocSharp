using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp.Elements
{
    public class InlineLiteral(ElementType name, string value, Location? location = null) : BaseInline(name)
    {
        public const string Type = "string";
        public string Value = value;

        public Location? Location = location;
    }
}
