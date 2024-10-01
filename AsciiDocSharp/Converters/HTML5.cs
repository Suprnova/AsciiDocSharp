using AsciiDocSharp.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AsciiDocSharp.Converters;

public class HTML5 : Converter
{
    public HTML5(bool xmlMode = false) : base()
    {
    }

    public static async Task<string> ConvertToHTML5(Elements.Document doc)
    {
        IServiceCollection services = new ServiceCollection();
        _ = services.AddLogging();

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        await using HtmlRenderer htmlRenderer = new(serviceProvider, loggerFactory);

        string a = String.Empty;
        await htmlRenderer.Dispatcher.InvokeAsync(async () =>
        {
            Dictionary<string, object?> dictionary = new()
            {
                { "doc", doc }
            };

            ParameterView parameters = ParameterView.FromDictionary(dictionary);
            Microsoft.AspNetCore.Components.Web.HtmlRendering.HtmlRootComponent output = await htmlRenderer.RenderComponentAsync<DocumentView>(parameters);

            a = output.ToHtmlString().ToString();
        });
        return a;
    }

    public override string Convert(AbstractNode node, string? transform = null, NotImplementedException? options = null)
    {
        IServiceCollection services = new ServiceCollection();
        _ = services.AddLogging();

        IServiceProvider serviceProvider = services.BuildServiceProvider();
        ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

        using HtmlRenderer htmlRenderer = new(serviceProvider, loggerFactory);

        string convertedText = String.Empty;
        _ = htmlRenderer.Dispatcher.InvokeAsync(() =>
        {
            KeyValuePair<string, object?> pair = GetPairFromNode(node);
            Dictionary<string, object?> dictionary = new()
            { { pair.Key, pair.Value } };

            ParameterView parameters = ParameterView.FromDictionary(dictionary);
            Task<Microsoft.AspNetCore.Components.Web.HtmlRendering.HtmlRootComponent> output = htmlRenderer.RenderComponentAsync<DocumentView>(parameters);
            output.Wait();

            convertedText = output.Result.ToHtmlString().ToString();
        });
        return convertedText;
    }

    private static KeyValuePair<string, object?> GetPairFromNode(AbstractNode node)
    {
        return node.Context switch
        {
            ElementType.Document => new("doc", node),
            _ => new("", null),
        };
    }
}
