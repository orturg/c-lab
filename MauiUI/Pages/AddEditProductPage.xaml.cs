using MauiUI.ViewModels;

namespace MauiUI.Pages;

public partial class AddEditProductPage : ContentPage
{
    private readonly AddEditProductViewModel _viewModel;

    public AddEditProductPage(AddEditProductViewModel viewModel)
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