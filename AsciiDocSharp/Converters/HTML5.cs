using AsciiDocSharp.Elements;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;

namespace AsciiDocSharp.Converters
{
    public class HTML5 : Converter
    {
        public HTML5(bool xmlMode = false) : base()
        {
        }

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

        public override string Convert(AbstractNode node, string? transform = null, NotImplementedException? options = null)
        {
            IServiceCollection services = new ServiceCollection();
            services.AddLogging();

            IServiceProvider serviceProvider = services.BuildServiceProvider();
            ILoggerFactory loggerFactory = serviceProvider.GetRequiredService<ILoggerFactory>();

            using var htmlRenderer = new HtmlRenderer(serviceProvider, loggerFactory);

            string convertedText = String.Empty;
            htmlRenderer.Dispatcher.InvokeAsync(() =>
            {
                KeyValuePair<string, object?> pair = GetPairFromNode(node);
				var dictionary = new Dictionary<string, object?> { { pair.Key, pair.Value } };

                var parameters = ParameterView.FromDictionary(dictionary);
                var output = htmlRenderer.RenderComponentAsync<DocumentView>(parameters);
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
}
