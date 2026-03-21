namespace Lab1.Services.DTO;

/// <summary>
/// DTO для відображення товару у списку
/// </summary>
public class ProductListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string CategoryDisplay { get; init; } = string.Empty;
    public int Quantity { get; init; }
    public decimal UnitPrice { get; init; }
}