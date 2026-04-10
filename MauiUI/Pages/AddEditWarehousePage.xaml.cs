using MauiUI.ViewModels;

namespace MauiUI.Pages;

public partial class AddEditWarehousePage : ContentPage
{
    private readonly AddEditWarehouseViewModel _viewModel;

    public AddEditWarehousePage(AddEditWarehouseViewModel viewModel)
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