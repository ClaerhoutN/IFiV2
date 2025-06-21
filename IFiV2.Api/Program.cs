using IFiV2.Api.Domain.Services;
using IFiV2.Api.Domain.Services.Interfaces;
using IFiV2.Common.Http;
using IFiV2.Models;
using Refit;
using System.Text.Json.Serialization;
using System.Text.RegularExpressions;

var builder = WebApplication.CreateSlimBuilder(args);

//builder.Services.ConfigureHttpJsonOptions(options =>
//{
//    options.SerializerOptions.TypeInfoResolverChain.Insert(0, AppJsonSerializerContext.Default);
//});

builder.Services.AddTransient<IStockMarketService, StockMarketService>();

builder.Services.AddRefitClient<IFiV2.Api.Domain.ApiServices.IEodHdService>()
                .ConfigureHttpClient(c =>
                {
                    c.BaseAddress = new Uri(builder.Configuration["EodHdApi:Url"]);
                })
                .AddHttpMessageHandler(() => new AddQueryStringArgumentHandler("api_token", builder.Configuration["EodHdApi:ApiToken"]));

var app = builder.Build();

var todosApi = app.MapGroup("/api/stockmarket");
todosApi.MapGet("/stock-data-point", async (IStockMarketService _stockMarketService, 
    string[] symbolsWithExchange, Interval interval, DateTimeOffset from, DateTimeOffset to) =>
    {
        var dataPoints = await _stockMarketService.GetStockDataPointsAsync(symbolsWithExchange, interval, from, to);
        return Results.Ok(dataPoints);
    });
todosApi.MapGet("/search", async (IStockMarketService _stockMarketService,
    string search) =>
{
    var stocks = await _stockMarketService.SearchAsync(search);
    return Results.Ok(stocks);
});

app.Run();

//[JsonSerializable(typeof(IReadOnlyList<StockDataPoint>))]
//[JsonSerializable(typeof(IReadOnlyList<IFiV2.Api.Domain.Dto.StockDataPoint>))]
//internal partial class AppJsonSerializerContext : JsonSerializerContext
//{

//}
