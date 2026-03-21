using Lab1.Models.Enums;
using Lab1.Repositories;
using Lab1.Services.DTO;

namespace Lab1.Services;

/// <summary>
/// Сервіс для отримання та перетворення даних складів і товарів
/// </summary>
public class WarehouseService(IWarehouseRepository repository) : IWarehouseService
{
    public List<WarehouseListDto> GetAllWarehouses() =>
        repository.GetAllWarehouses()
            .Select(w => new WarehouseListDto
            {
                Id = w.Id,
                Name = w.Name,
                LocationDisplay = LocationToUkrainian(w.Location)
            })
            .ToList();

    public WarehouseDetailDto? GetWarehouseById(int id)
    {
        var warehouse = repository.GetWarehouseById(id);
        if (warehouse is null) return null;

        var products = repository.GetProductsByWarehouseId(id)
            .Select(p => new ProductListDto
            {
                Id = p.Id,
                Name = p.Name,
                CategoryDisplay = CategoryToUkrainian(p.Category),
                Quantity = p.Quantity,
                UnitPrice = p.UnitPrice
            })
            .ToList();

        return new WarehouseDetailDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            LocationDisplay = LocationToUkrainian(warehouse.Location),
            TotalValue = products.Sum(p => p.UnitPrice * p.Quantity),
            Products = products
        };
    }

    public ProductDetailDto? GetProductById(int warehouseId, int productId)
    {
        var product = repository.GetProductById(warehouseId, productId);
        if (product is null) return null;

        return new ProductDetailDto
        {
            Id = product.Id,
            WarehouseId = product.WarehouseId,
            Name = product.Name,
            CategoryDisplay = CategoryToUkrainian(product.Category),
            Quantity = product.Quantity,
            UnitPrice = product.UnitPrice,
            TotalValue = product.UnitPrice * product.Quantity,
            Description = product.Description
        };
    }

    private static string LocationToUkrainian(WarehouseLocation location) => location switch
    {
        WarehouseLocation.Kyiv => "Київ",
        WarehouseLocation.Lviv => "Львів",
        WarehouseLocation.Odesa => "Одеса",
        WarehouseLocation.Kharkiv => "Харків",
        WarehouseLocation.Dnipro => "Дніпро",
        _ => location.ToString()
    };

    private static string CategoryToUkrainian(ProductCategory category) => category switch
    {
        ProductCategory.Electronics => "Електроніка",
        ProductCategory.Clothing => "Одяг",
        ProductCategory.Food => "Продукти",
        ProductCategory.Furniture => "Меблі",
        ProductCategory.Tools => "Інструменти",
        _ => category.ToString()
    };
}
