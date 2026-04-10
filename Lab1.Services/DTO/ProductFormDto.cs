using Lab1.Models.Enums;

namespace Lab1.Services.DTO;

/// <summary>
/// DTO для форми створення або редагування продукту
/// </summary>
public class ProductFormDto
{
    public int Id { get; set; }
    public int WarehouseId { get; set; }
    public string Name { get; set; } = string.Empty;
    public int Quantity { get; set; }
    public decimal UnitPrice { get; set; }
    public ProductCategory Category { get; set; }
    public string Description { get; set; } = string.Empty;
}