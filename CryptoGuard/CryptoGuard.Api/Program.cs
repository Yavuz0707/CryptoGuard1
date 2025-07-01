using CryptoGuard.Core.Interfaces;
using CryptoGuard.Core.Models;
using CryptoGuard.Infrastructure.Services;
using Microsoft.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);

// Logging ve ICoinLoreService ekle
builder.Services.AddLogging();
builder.Services.AddSingleton<ICoinLoreService, CoinLoreService>();

var app = builder.Build();

app.MapGet("/api/coins", async (ICoinLoreService coinService) =>
{
    var coins = await coinService.GetTopCoinsAsync(100);
    return Results.Ok(coins);
});

app.MapGet("/api/coins/{id}", async (string id, ICoinLoreService coinService) =>
{
    var coin = await coinService.GetCoinPriceAsync(id);
    return coin is not null ? Results.Ok(coin) : Results.NotFound();
});

app.MapGet("/api/coins/{id}/marketcap", async (string id, ICoinLoreService coinService) =>
{
    try
    {
        var marketCap = await coinService.GetCoinMarketCapAsync(id);
        return Results.Ok(marketCap);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/api/coins/{id}/change24h", async (string id, ICoinLoreService coinService) =>
{
    try
    {
        var change = await coinService.GetCoinPriceChangePercentage24hAsync(id);
        return Results.Ok(change);
    }
    catch (Exception ex)
    {
        return Results.Problem(ex.Message);
    }
});

app.MapGet("/", () => "CryptoGuard Minimal API Çalışıyor!");

app.Run();
