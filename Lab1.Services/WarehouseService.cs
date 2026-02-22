using Lab1.ViewModels;

namespace Lab1.Services;

/// <summary>
/// Сервіс роботи зі штучною бд
/// </summary>
public class WarehouseService
{
    public List<WarehouseViewModel> GetAllWarehouses()
    {
        return FakeStorage.Warehouses
            .Select(model => new WarehouseViewModel(model))
            .ToList();
    }
    
    public WarehouseViewModel? GetWarehouseById(int id)
    {
        var model = FakeStorage.Warehouses.FirstOrDefault(w => w.Id == id);
        return model is null ? null : new WarehouseViewModel(model);
    }
    
    public void LoadProductsForWarehouse(WarehouseViewModel warehouse)
    {
        if (warehouse.ProductsLoaded) return;
        
        var products = FakeStorage.Products
            .Where(p => p.WarehouseId == warehouse.Id)
            .Select(p => new ProductViewModel(p))
            .ToList();
        
        warehouse.LoadProducts(products);
    }

    public List<ProductViewModel> GetProductsByWarehouseId(int warehouseId)
    {
        return FakeStorage.Products
            .Where(p => p.WarehouseId == warehouseId)
            .Select(p => new ProductViewModel(p))
            .ToList();
    }
}