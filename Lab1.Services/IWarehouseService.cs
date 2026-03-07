using Lab1.ViewModels;

namespace Lab1.Services;

/// <summary>
/// Інтерфейс сервісу для роботи зі складом та товарами
/// </summary>
public interface IWarehouseService
{
    List<WarehouseViewModel> GetAllWarehouses();
    WarehouseViewModel? GetWarehouseById(int id);
    void LoadProductsForWarehouse(WarehouseViewModel warehouse);
    List<ProductViewModel> GetProductsByWarehouseId(int warehouseId);
}