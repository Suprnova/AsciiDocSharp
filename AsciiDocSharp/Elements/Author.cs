namespace AsciiDocSharp.Elements;

public class Author
{
    public string? FullName;
    public string? Initials;
    public string? FirstName;
    public string? MiddleName;
    public string? LastName;
    public string? Address;

    public Author(string s)
    {
        string[] split = s.Split(' ');
        FirstName = split[0];
        if (split[^1].StartsWith('<') && split[^1].EndsWith('>')) {
            Address = split[^1][1..^1];
        }
        LastName = split[Address is null ? ^1 : ^2];
        if (split[1] != LastName) {
            MiddleName = split[1];
        }
    }
}
