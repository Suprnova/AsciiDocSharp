using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Section : AbstractHeading
    {
        public const string Name = "section";
        public required SectionBody[] Blocks { get; set; } = [];
    }
}
