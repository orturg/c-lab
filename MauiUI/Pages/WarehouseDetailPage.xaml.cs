using MauiUI.ViewModels;

namespace MauiUI.Pages;

/// <summary>
/// Сторінка деталей складу, відображає інформацію про склад та товари на складі
/// </summary>

public partial class WarehouseDetailPage : ContentPage
{
    public WarehouseDetailPage(WarehouseDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
