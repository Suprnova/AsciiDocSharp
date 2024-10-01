namespace AsciiDocSharp;

#pragma warning disable IDE0044 // Add readonly modifier
#pragma warning disable IDE0051 // Remove unused private members
internal class Reader(string data)
{
    private int lineNumber = 0;
#pragma warning disable IDE0052 // Remove unread private members
    private string[] lines = NormalizeLines(data);
#pragma warning restore IDE0052 // Remove unread private members

    private static string[] NormalizeLines(string data) => NormalizeLines(data.Split('\n'));

    private static string[] NormalizeLines(string[] data)
    {
        bool inCommentBlock = false;
        List<string> normalizedData = [];
        foreach (string line in data) {
            if (line.StartsWith("////")) {
                inCommentBlock = !inCommentBlock;
                continue;
            }
            if (line.StartsWith("//"))
                continue;
            normalizedData.Add(line.TrimEnd());
        }
        return [.. normalizedData];
    }
}
#pragma warning restore IDE0044 // Add readonly modifier
#pragma warning restore IDE0051 // Remove unused private members
