using Lab1.Models;

namespace Lab1.Repositories;

/// <summary>
/// Інтерфейс репозиторію для роботи зі складами та товарами
/// </summary>
public interface IWarehouseRepository
{
    Task<List<WarehouseModel>> GetAllWarehousesAsync();
    Task<WarehouseModel?> GetWarehouseByIdAsync(int id);
    Task<List<ProductModel>> GetProductsByWarehouseIdAsync(int warehouseId);
    Task<ProductModel?> GetProductByIdAsync(int warehouseId, int productId);

    Task<WarehouseModel> AddWarehouseAsync(WarehouseModel warehouse);
    Task UpdateWarehouseAsync(WarehouseModel warehouse);
    Task DeleteWarehouseAsync(int id);

    Task<ProductModel> AddProductAsync(ProductModel product);
    Task UpdateProductAsync(ProductModel product);
    Task DeleteProductAsync(int productId);
}