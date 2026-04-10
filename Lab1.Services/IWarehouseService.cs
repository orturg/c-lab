using Lab1.Models.Enums;
using Lab1.Services.DTO;

namespace Lab1.Services;

/// <summary>
/// Інтерфейс сервісу для роботи зі складами та товарами
/// </summary>
public interface IWarehouseService
{
    Task<List<WarehouseListDto>> GetAllWarehousesAsync();
    Task<WarehouseDetailDto?> GetWarehouseByIdAsync(int id);
    Task<WarehouseFormDto?> GetWarehouseFormAsync(int id);
    Task<WarehouseListDto> AddWarehouseAsync(string name, WarehouseLocation location);
    Task UpdateWarehouseAsync(int id, string name, WarehouseLocation location);
    Task DeleteWarehouseAsync(int id);

    Task<ProductDetailDto?> GetProductByIdAsync(int warehouseId, int productId);
    Task<ProductFormDto?> GetProductFormAsync(int warehouseId, int productId);
    Task<ProductListDto> AddProductAsync(int warehouseId, string name, int quantity, decimal unitPrice, ProductCategory category, string description);
    Task UpdateProductAsync(int warehouseId, int productId, string name, int quantity, decimal unitPrice, ProductCategory category, string description);
    Task DeleteProductAsync(int warehouseId, int productId);
}