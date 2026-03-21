using MauiUI.ViewModels;

namespace MauiUI.Pages;
/// <summary>
/// Сторінка товару, відображаються всі дані про товар
/// </summary>

public partial class ProductDetailPage : ContentPage
{
    public ProductDetailPage(ProductDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
