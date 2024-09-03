using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class SectionBody
    {
        public AbstractBlock[] Body
        {
            get { return Body; }
            set
            {
                foreach (var block in value)
                {
                    if (block is not Block && block is not Section)
                    {
                        throw new InvalidOperationException();
                    }

                }
            }
        }
    }
}
