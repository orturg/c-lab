using System.Windows.Input;
using Lab1.Services;
using Lab1.Services.DTO;
using MauiUI.Pages;

namespace MauiUI.ViewModels;

/// <summary>
/// vm для сторінки деталей складу з переліком товарів
/// </summary>
public class WarehouseDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IWarehouseService _warehouseService;

    private WarehouseDetailDto? _warehouse;
    public WarehouseDetailDto? Warehouse
    {
        get => _warehouse;
        set => SetProperty(ref _warehouse, value);
    }
    
    public ICommand SelectProductCommand { get; }

    public WarehouseDetailViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
        SelectProductCommand = new Command<ProductListDto>(OnProductSelected);
    }
    
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var value) &&
            int.TryParse(value?.ToString(), out var warehouseId))
        {
            Warehouse = _warehouseService.GetWarehouseById(warehouseId);
        }
    }

    private async void OnProductSelected(ProductListDto product)
    {
        if (product is null || Warehouse is null) return;
        await Shell.Current.GoToAsync(
            $"{nameof(ProductDetailPage)}?warehouseId={Warehouse.Id}&productId={product.Id}");
    }
}