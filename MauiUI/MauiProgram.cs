using Lab1.Repositories;
using Lab1.Services;
using Microsoft.Extensions.Logging;
using MauiUI.Pages;
using MauiUI.ViewModels;

namespace MauiUI;

/// <summary>
/// Точка входу maui
/// </summary>
public static class MauiProgram
{
    public static MauiApp CreateMauiApp()
    {
        var builder = MauiApp.CreateBuilder();
        builder
            .UseMauiApp<App>()
            .ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
            });

        builder.Services.AddSingleton<IWarehouseRepository, WarehouseRepository>();

        builder.Services.AddSingleton<IWarehouseService, WarehouseService>();

        builder.Services.AddTransient<WarehousesViewModel>();
        builder.Services.AddTransient<WarehouseDetailViewModel>();
        builder.Services.AddTransient<ProductDetailViewModel>();

        builder.Services.AddSingleton<AppShell>();
        builder.Services.AddTransient<WarehousesPage>();
        builder.Services.AddTransient<WarehouseDetailPage>();
        builder.Services.AddTransient<ProductDetailPage>();

#if DEBUG
        builder.Logging.AddDebug();
#endif

        return builder.Build();
    }
}