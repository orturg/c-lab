using Lab1.Services;
using Lab1.ViewModels;

namespace MauiUI.Pages;

/// <summary>
/// Сторінка, що відображає список усіх складів
/// </summary>
public partial class WarehousesPage : ContentPage
{
    private readonly IWarehouseService _warehouseService;

    public WarehousesPage(IWarehouseService warehouseService)
    {
        InitializeComponent();
        _warehouseService = warehouseService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        WarehousesCollection.ItemsSource = _warehouseService.GetAllWarehouses();
    }

    private async void OnWarehouseSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not WarehouseViewModel warehouse)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync($"{nameof(WarehouseDetailPage)}?warehouseId={warehouse.Id}");
    }
}