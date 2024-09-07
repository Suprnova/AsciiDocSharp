using AsciiDocSharp.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace AsciiDocSharp.Converters
{
    public class HTML5
    {
        public static async Task<string> ConvertToHTML5(Elements.Document doc)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            await using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

            string a = String.Empty;
            await htmlRenderer.Dispatcher.InvokeAsync(async () =>
            {
                var dictionary = new Dictionary<string, object?>
                {
                    { "doc", doc }
                };

                var parameters = ParameterView.FromDictionary(dictionary);
                var output = await htmlRenderer.RenderComponentAsync<DocumentView>(parameters);

                a = output.ToHtmlString().ToString();
            });
            return a;
        }
    }
}
