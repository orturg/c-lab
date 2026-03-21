using Lab1.Services.DTO;

namespace Lab1.Services;

/// <summary>
/// Інтерфейс сервісу для роботи зі складами та товарами
/// </summary>
public interface IWarehouseService
{
    List<WarehouseListDto> GetAllWarehouses();

    WarehouseDetailDto? GetWarehouseById(int id);

    ProductDetailDto? GetProductById(int warehouseId, int productId);
}