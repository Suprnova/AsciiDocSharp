using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using AsciiDocSharp.Elements;

namespace AsciiDocSharp
{
    public class Parser
    {
        public static void Parse(string input)
        {
            input = input.ReplaceLineEndings("\n");
            Document? doc = null;
            var lines = input.Split('\n');
            StringBuilder buffer = new StringBuilder();

            foreach (var line in lines)
            {
                if (String.IsNullOrWhiteSpace(line))
                {
                    doc = new Document(buffer.ToString().ReplaceLineEndings("\n").Split('\n')[..^1]);
                    buffer.Clear();
                    break;
                }
                buffer.AppendLine(line);
            }

            System.Console.WriteLine(doc == null ? doc : "boobs");
        }
    }
}
