using MauiUI.ViewModels;

namespace MauiUI.Pages;

public partial class WarehousesPage : ContentPage
{
    private readonly WarehousesViewModel _viewModel;

    public WarehousesPage(WarehousesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadWarehousesAsync();
    }
}