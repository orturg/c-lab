namespace Lab1.Services.DTO;

/// <summary>
/// DTO для відображення детальної інформації про склад з переліком товарів
/// </summary>
public class WarehouseDetailDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string LocationDisplay { get; init; } = string.Empty;
    public decimal TotalValue { get; init; }
    public List<ProductListDto> Products { get; init; } = [];
}