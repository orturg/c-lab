using Microsoft.Extensions.Logging;
using Lab1.Services;
using MauiUI.Pages;

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
        
        builder.Services.AddSingleton<IWarehouseService, WarehouseService>();
        
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