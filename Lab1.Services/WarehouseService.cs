using Lab1.Models;
using Lab1.Models.Enums;
using Lab1.Repositories;
using Lab1.Services.DTO;

namespace Lab1.Services;

/// <summary>
/// Сервіс для отримання та перетворення даних складів і товарів
/// </summary>
public class WarehouseService(IWarehouseRepository repository) : IWarehouseService
{
    public async Task<List<WarehouseListDto>> GetAllWarehousesAsync()
    {
        var warehouses = await repository.GetAllWarehousesAsync();
        return warehouses.Select(ToWarehouseListDto).ToList();
    }

    public async Task<WarehouseDetailDto?> GetWarehouseByIdAsync(int id)
    {
        var warehouse = await repository.GetWarehouseByIdAsync(id);
        if (warehouse is null) return null;

        var products = await repository.GetProductsByWarehouseIdAsync(id);
        var productDtos = products.Select(ToProductListDto).ToList();

        return new WarehouseDetailDto
        {
            Id = warehouse.Id,
            Name = warehouse.Name,
            LocationDisplay = LocationToUkrainian(warehouse.Location),
            TotalValue = productDtos.Sum(p => p.UnitPrice * p.Quantity),
            Products = productDtos
        };
    }

    public async Task<WarehouseFormDto?> GetWarehouseFormAsync(int id)
    {
        var warehouse = await repository.GetWarehouseByIdAsync(id);
        if (warehouse is null) return null;
        return new WarehouseFormDto { Id = warehouse.Id, Name = warehouse.Name, Location = warehouse.Location };
    }

    public async Task<WarehouseListDto> AddWarehouseAsync(string name, WarehouseLocation location)
    {
        var saved = await repository.AddWarehouseAsync(new WarehouseModel(0, name, location));
        return ToWarehouseListDto(saved);
    }

    public async Task UpdateWarehouseAsync(int id, string name, WarehouseLocation location)
        => await repository.UpdateWarehouseAsync(new WarehouseModel(id, name, location));

    public async Task DeleteWarehouseAsync(int id)
        => await repository.DeleteWarehouseAsync(id);

    public async Task<ProductDetailDto?> GetProductByIdAsync(int warehouseId, int productId)
    {
        var product = await repository.GetProductByIdAsync(warehouseId, productId);
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

    public async Task<ProductFormDto?> GetProductFormAsync(int warehouseId, int productId)
    {
        var product = await repository.GetProductByIdAsync(warehouseId, productId);
        if (product is null) return null;
        return new ProductFormDto
        {
            Id = product.Id,
            WarehouseId = product.WarehouseId,
            Name = product.Name,
            Quantity = product.Quantity,
            UnitPrice = product.UnitPrice,
            Category = product.Category,
            Description = product.Description
        };
    }

    public async Task<ProductListDto> AddProductAsync(int warehouseId, string name, int quantity,
        decimal unitPrice, ProductCategory category, string description)
    {
        var saved = await repository.AddProductAsync(
            new ProductModel(0, warehouseId, name, quantity, unitPrice, category, description));
        return ToProductListDto(saved);
    }

    public async Task UpdateProductAsync(int warehouseId, int productId, string name, int quantity,
        decimal unitPrice, ProductCategory category, string description)
        => await repository.UpdateProductAsync(
            new ProductModel(productId, warehouseId, name, quantity, unitPrice, category, description));

    public async Task DeleteProductAsync(int warehouseId, int productId)
        => await repository.DeleteProductAsync(productId);

    private static WarehouseListDto ToWarehouseListDto(WarehouseModel w) => new()
    {
        Id = w.Id, Name = w.Name, LocationDisplay = LocationToUkrainian(w.Location)
    };

    private static ProductListDto ToProductListDto(ProductModel p) => new()
    {
        Id = p.Id, Name = p.Name, CategoryDisplay = CategoryToUkrainian(p.Category),
        Quantity = p.Quantity, UnitPrice = p.UnitPrice
    };

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
