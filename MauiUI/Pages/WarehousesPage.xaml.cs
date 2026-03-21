using MauiUI.ViewModels;

namespace MauiUI.Pages;

/// <summary>
/// Сторінка, що відображає список усіх складів
/// </summary>
public partial class WarehousesPage : ContentPage
{
    public WarehousesPage(WarehousesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
