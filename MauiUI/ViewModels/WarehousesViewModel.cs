using System.Collections.ObjectModel;
using System.Windows.Input;
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

    private ObservableCollection<WarehouseListDto> _warehouses = [];

    public ObservableCollection<WarehouseListDto> Warehouses
    {
        get => _warehouses;
        private set => SetProperty(ref _warehouses, value);
    }
    public ICommand SelectWarehouseCommand { get; }

    public WarehousesViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
        SelectWarehouseCommand = new Command<WarehouseListDto>(OnWarehouseSelected);
        LoadWarehouses();
    }

    private void LoadWarehouses()
    {
        var warehouses = _warehouseService.GetAllWarehouses();
        Warehouses = new ObservableCollection<WarehouseListDto>(warehouses);
    }

    private async void OnWarehouseSelected(WarehouseListDto warehouse)
    {
        if (warehouse is null) return;
        await Shell.Current.GoToAsync($"{nameof(WarehouseDetailPage)}?warehouseId={warehouse.Id}");
    }
}