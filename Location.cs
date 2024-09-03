using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Location
    {
        public required LocationBoundary Lower;
        public required LocationBoundary Upper;
    }

    public class LocationBoundary
    {
        public required int line;
        public required int col;
        // Apparently can contain multiple files?
        public string[]? file;
    }
}
