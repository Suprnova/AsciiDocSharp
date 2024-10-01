using AsciiDocSharp.Elements;

namespace AsciiDocSharp.Converters;

public abstract class Converter()
{
    public abstract string Convert(AbstractNode node, string? transform = null, NotImplementedException? options = null);
}
