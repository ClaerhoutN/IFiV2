using IFiV2.Services;
using IFiV2.Shared.Services;
using IFiV2.Shared.Services.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;
using Refit;
using System.Reflection;
using System.Text.RegularExpressions;

namespace IFiV2
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();

            var sharedRootType = typeof(IFiV2.Shared.Routes);
            var sharedAssembly = sharedRootType.Assembly;
            using var stream = sharedAssembly.GetManifestResourceStream(sharedRootType.Namespace + ".appsettings.json");
            var config = new ConfigurationBuilder()
                        .AddJsonStream(stream)
                        .Build();
            builder.Configuration.AddConfiguration(config);

            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddSingleton<IFormFactor, FormFactor>();
            builder.Services.AddSingleton<IStockMarketService, StockMarketService>();

            var clientBuilder = builder.Services.AddRefitClient<Shared.ApiServices.IStockMarketService>()
                .ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration["StockMarketApi:Url"]));
#if DEBUG
            if (Regex.IsMatch(builder.Configuration["StockMarketApi:Url"], "https:\\/\\/localhost:\\d+.+"))
            {
                clientBuilder.ConfigurePrimaryHttpMessageHandler(() =>
                {
                    return new HttpClientHandler
                    {
                        ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
                    };
                });
            }
#endif

            builder.Services.AddMudServices();
            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif

            return builder.Build();
        }
    }
}
