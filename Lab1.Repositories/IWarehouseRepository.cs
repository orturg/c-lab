using Lab1.Models;

namespace Lab1.Repositories;

/// <summary>
/// Інтерфейс репозиторію для роботи зі складами та товарами
/// </summary>
public interface IWarehouseRepository
{
    List<WarehouseModel> GetAllWarehouses();

    WarehouseModel? GetWarehouseById(int id);

    List<ProductModel> GetProductsByWarehouseId(int warehouseId);

    ProductModel? GetProductById(int warehouseId, int productId);
}