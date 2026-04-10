using MauiUI.ViewModels;

namespace MauiUI.Pages;

public partial class WarehouseDetailPage : ContentPage
{
    private readonly WarehouseDetailViewModel _viewModel;

    public WarehouseDetailPage(WarehouseDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        await _viewModel.LoadDataAsync();
    }
}