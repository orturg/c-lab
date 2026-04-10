using System.Windows.Input;
using Lab1.Services;
using Lab1.Services.DTO;
using MauiUI.Pages;

namespace MauiUI.ViewModels;

/// <summary>
/// vm для сторінки детальної інформації про товар
/// </summary>
public class ProductDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IWarehouseService _warehouseService;
    private int _warehouseId;
    private int _productId;

    private ProductDetailDto? _product;
    public ProductDetailDto? Product
    {
        get => _product;
        private set => SetProperty(ref _product, value);
    }

    public ICommand LoadDataCommand { get; }
    public ICommand EditProductCommand { get; }

    public ProductDetailViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
        LoadDataCommand = new Command(async () => await LoadDataAsync());
        EditProductCommand = new Command(async () =>
            await Shell.Current.GoToAsync(
                $"{nameof(AddEditProductPage)}?warehouseId={_warehouseId}&productId={_productId}"));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var wVal))
            int.TryParse(wVal?.ToString(), out _warehouseId);
        if (query.TryGetValue("productId", out var pVal))
            int.TryParse(pVal?.ToString(), out _productId);
    }

    public async Task LoadDataAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Product = await _warehouseService.GetProductByIdAsync(_warehouseId, _productId);
        }
        finally
        {
            IsBusy = false;
        }
    }
}