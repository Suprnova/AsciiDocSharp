using AsciiDocSharp;
using AsciiDocSharp.Elements;

class Program
{
    static void Main(string[] args)
    {
        Parser.Parse("// this comment line is ignored\n" +
            "= Document Title \n" +
            "Kismet R. Lee <kismet@asciidoctor.org> " +
            "\n:description: The document's description. " +
            "\n:sectanchors: " +
            "\n:url-repo: https://my-git-repo.com " +
            "\n" +
            "\nThe document body starts here.");
    }
}