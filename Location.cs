using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AsciiDocSharp
{
    public class Location(int lowerLine, int lowerCol, int upperLine, int upperCol, string[]? file = null)
    {
        public LocationBoundary Lower = new(lowerLine, lowerCol, file);
        public LocationBoundary Upper = new(upperLine, upperCol, file);
    }

    public class LocationBoundary(int line, int col, string[]? file = null)
    {
        public int Line = line;
        public int Col = col;
        // Apparently can contain multiple files?
        public string[]? File = file;
    }
}
