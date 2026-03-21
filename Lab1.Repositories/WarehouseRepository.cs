using Lab1.Models;
using Lab1.Repositories.Storage;

namespace Lab1.Repositories;

/// <summary>
/// Репозиторій для доступу до даних складів та товарів
/// </summary>
public class WarehouseRepository : IWarehouseRepository
{
    public List<WarehouseModel> GetAllWarehouses() =>
        FakeStorage.Warehouses.ToList();

    public WarehouseModel? GetWarehouseById(int id) =>
        FakeStorage.Warehouses.FirstOrDefault(w => w.Id == id);

    public List<ProductModel> GetProductsByWarehouseId(int warehouseId) =>
        FakeStorage.Products.Where(p => p.WarehouseId == warehouseId).ToList();

    public ProductModel? GetProductById(int warehouseId, int productId) =>
        FakeStorage.Products.FirstOrDefault(p => p.WarehouseId == warehouseId && p.Id == productId);
}