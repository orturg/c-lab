using Lab1.Services;
using Lab1.ViewModels;

namespace MauiUI.Pages;

/// <summary>
/// Сторінка деталей складу, відображає інформацію про склад та товари на складі
/// </summary>

[QueryProperty(nameof(WarehouseId), "warehouseId")]
public partial class WarehouseDetailPage : ContentPage
{
    private readonly IWarehouseService _warehouseService;
    private WarehouseViewModel? _warehouse;

    public int WarehouseId
    {
        set => LoadWarehouse(value);
    }

    public WarehouseDetailPage(IWarehouseService warehouseService)
    {
        InitializeComponent();
        _warehouseService = warehouseService;
    }

    private void LoadWarehouse(int id)
    {
        _warehouse = _warehouseService.GetWarehouseById(id);
        if (_warehouse is null)
            return;

        _warehouseService.LoadProductsForWarehouse(_warehouse);

        WarehouseNameLabel.Text = $"Склад #{_warehouse.Id}: {_warehouse.Name}";
        WarehouseLocationLabel.Text = $"Місто: {_warehouse.LocationDisplay}";
        WarehouseTotalLabel.Text = $"Загальна вартість: {_warehouse.TotalValue:N2} грн";

        ProductsCollection.ItemsSource = _warehouse.Products;
    }

    private async void OnProductSelected(object sender, SelectionChangedEventArgs e)
    {
        if (e.CurrentSelection.FirstOrDefault() is not ProductViewModel product)
            return;

        ((CollectionView)sender).SelectedItem = null;
        await Shell.Current.GoToAsync(
            $"{nameof(ProductDetailPage)}?warehouseId={_warehouse!.Id}&productId={product.Id}");
    }
}