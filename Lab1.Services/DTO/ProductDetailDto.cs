namespace Lab1.Services.DTO;

/// <summary>
/// DTO для відображення детальної інформації про товар
/// </summary>
public class ProductDetailDto
{
    public int Id { get; init; }
    public int WarehouseId { get; init; }
    public string Name { get; init; } = string.Empty;
    public string CategoryDisplay { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
    public decimal TotalValue { get; init; }
    public string Description { get; init; } = string.Empty;
}