using System.Collections.ObjectModel;
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
    private int _warehouseId;
    private List<ProductListDto> _allProducts = [];

    private WarehouseDetailDto? _warehouse;
    public WarehouseDetailDto? Warehouse
    {
        get => _warehouse;
        private set => SetProperty(ref _warehouse, value);
    }

    public ObservableCollection<ProductListDto> FilteredProducts { get; } = [];

    private string _searchText = string.Empty;
    public string SearchText
    {
        get => _searchText;
        set { SetProperty(ref _searchText, value); ApplyFilter(); }
    }

    private int _sortIndex;
    public int SortIndex
    {
        get => _sortIndex;
        set { SetProperty(ref _sortIndex, value); ApplyFilter(); }
    }

    public ICommand LoadDataCommand { get; }
    public ICommand EditWarehouseCommand { get; }
    public ICommand AddProductCommand { get; }
    public ICommand DeleteProductCommand { get; }
    public ICommand SelectProductCommand { get; }

    public WarehouseDetailViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;

        LoadDataCommand = new Command(async () => await LoadDataAsync());
        EditWarehouseCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(AddEditWarehousePage)}?warehouseId={_warehouseId}"));
        AddProductCommand = new Command(async () =>
            await Shell.Current.GoToAsync($"{nameof(AddEditProductPage)}?warehouseId={_warehouseId}"));
        DeleteProductCommand = new Command<ProductListDto>(async p => await DeleteProductAsync(p));
        SelectProductCommand = new Command<ProductListDto>(async p => await SelectProductAsync(p));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var value) &&
            int.TryParse(value?.ToString(), out var id))
            _warehouseId = id;
    }

    public async Task LoadDataAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            Warehouse = await _warehouseService.GetWarehouseByIdAsync(_warehouseId);
            _allProducts = Warehouse?.Products ?? [];
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplyFilter()
    {
        var filtered = _allProducts.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(p =>
                p.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                p.CategoryDisplay.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        filtered = SortIndex switch
        {
            0 => filtered.OrderBy(p => p.Name),
            1 => filtered.OrderByDescending(p => p.Name),
            2 => filtered.OrderBy(p => p.UnitPrice),
            3 => filtered.OrderByDescending(p => p.UnitPrice),
            4 => filtered.OrderBy(p => p.Quantity),
            5 => filtered.OrderByDescending(p => p.Quantity),
            _ => filtered
        };

        FilteredProducts.Clear();
        foreach (var p in filtered)
            FilteredProducts.Add(p);
    }


    private async Task DeleteProductAsync(ProductListDto product)
    {
        if (IsBusy) return;
        var confirm = await Shell.Current.DisplayAlert(
            "Видалення", $"Видалити товар «{product.Name}»?", "Так", "Ні");
        if (!confirm) return;

        IsBusy = true;
        try
        {
            await _warehouseService.DeleteProductAsync(_warehouseId, product.Id);
            _allProducts.Remove(product);
            ApplyFilter();
            Warehouse = await _warehouseService.GetWarehouseByIdAsync(_warehouseId);
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task SelectProductAsync(ProductListDto product)
    {
        if (product is null) return;
        await Shell.Current.GoToAsync(
            $"{nameof(ProductDetailPage)}?warehouseId={_warehouseId}&productId={product.Id}");
    }
}
