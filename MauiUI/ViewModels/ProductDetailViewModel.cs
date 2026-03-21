using Lab1.Services;
using Lab1.Services.DTO;
using static System.Int32;

namespace MauiUI.ViewModels;

/// <summary>
/// vm для сторінки детальної інформації про товар
/// </summary>
public class ProductDetailViewModel(IWarehouseService warehouseService) : BaseViewModel, IQueryAttributable
{
    private int _warehouseId;
    private int _productId;

    private ProductDetailDto? _product;

    public ProductDetailDto? Product
    {
        get => _product;
        private set => SetProperty(ref _product, value);
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var wVal))
            TryParse(wVal?.ToString(), out _warehouseId);

        if (query.TryGetValue("productId", out var pVal))
            TryParse(pVal?.ToString(), out _productId);

        Product = warehouseService.GetProductById(_warehouseId, _productId);
    }
}