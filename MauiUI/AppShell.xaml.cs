using MauiUI.Pages;

namespace MauiUI;

/// <summary>
/// Головна оболонка застосунку, яка відповідає за реєстраці маршрутів shell навігації
/// </summary>
public partial class AppShell : Shell
{
    public AppShell()
    {
        InitializeComponent();

        Routing.RegisterRoute(nameof(WarehouseDetailPage), typeof(WarehouseDetailPage));
        Routing.RegisterRoute(nameof(ProductDetailPage), typeof(ProductDetailPage));
    }
}