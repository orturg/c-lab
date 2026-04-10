using System.Collections.ObjectModel;
using System.Windows.Input;
using Lab1.Models.Enums;
using Lab1.Services;
using Lab1.Services.DTO;
using MauiUI.Pages;

namespace MauiUI.ViewModels;

/// <summary>
/// Vm для сторінки списку складів
/// </summary>
public class WarehousesViewModel : BaseViewModel
{
    private readonly IWarehouseService _warehouseService;
    private List<WarehouseListDto> _allWarehouses = [];

    public ObservableCollection<WarehouseListDto> FilteredWarehouses { get; } = [];

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

    public ICommand LoadWarehousesCommand { get; }
    public ICommand AddWarehouseCommand { get; }
    public ICommand DeleteWarehouseCommand { get; }
    public ICommand SelectWarehouseCommand { get; }

    public WarehousesViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;

        LoadWarehousesCommand = new Command(async () => await LoadWarehousesAsync());
        AddWarehouseCommand = new Command(async () =>
            await Shell.Current.GoToAsync(nameof(AddEditWarehousePage)));
        DeleteWarehouseCommand = new Command<WarehouseListDto>(async w => await DeleteWarehouseAsync(w));
        SelectWarehouseCommand = new Command<WarehouseListDto>(async w => await SelectWarehouseAsync(w));
    }

    public async Task LoadWarehousesAsync()
    {
        if (IsBusy) return;
        IsBusy = true;
        try
        {
            _allWarehouses = await _warehouseService.GetAllWarehousesAsync();
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private void ApplyFilter()
    {
        var filtered = _allWarehouses.AsEnumerable();

        if (!string.IsNullOrWhiteSpace(SearchText))
            filtered = filtered.Where(w =>
                w.Name.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                w.LocationDisplay.Contains(SearchText, StringComparison.OrdinalIgnoreCase));

        filtered = SortIndex switch
        {
            0 => filtered.OrderBy(w => w.Name),
            1 => filtered.OrderByDescending(w => w.Name),
            2 => filtered.OrderBy(w => w.LocationDisplay),
            3 => filtered.OrderByDescending(w => w.LocationDisplay),
            _ => filtered
        };

        FilteredWarehouses.Clear();
        foreach (var w in filtered)
            FilteredWarehouses.Add(w);
    }

    private async Task DeleteWarehouseAsync(WarehouseListDto warehouse)
    {
        if (IsBusy) return;
        var confirm = await Shell.Current.DisplayAlert(
            "Видалення", $"Видалити склад «{warehouse.Name}» та всі його товари?", "Так", "Ні");
        if (!confirm) return;

        IsBusy = true;
        try
        {
            await _warehouseService.DeleteWarehouseAsync(warehouse.Id);
            _allWarehouses.Remove(warehouse);
            ApplyFilter();
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task SelectWarehouseAsync(WarehouseListDto warehouse)
    {
        if (warehouse is null) return;
        await Shell.Current.GoToAsync($"{nameof(WarehouseDetailPage)}?warehouseId={warehouse.Id}");
    }
}
