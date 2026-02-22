using Lab1.Models.Enums;
namespace Lab1.Models;

public class ProductModel(
    int id,
    int warehouseId,
    string name,
    int quantity,
    decimal unitPrice,
    ProductCategory category,
    string description)
{
    public int Id { get; } = id;
    public int WarehouseId { get; set; } = warehouseId;
    public string Name { get; set; } = name;
    public int Quantity { get; set; } = quantity;
    public decimal UnitPrice { get; set; } = unitPrice;
    public ProductCategory Category { get; set; } = category;
    public string Description { get; set; } = description;
}