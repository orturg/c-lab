namespace Lab1.Services.DTO;

/// <summary>
/// DTo для відображення складу у списку
/// </summary>
public class WarehouseListDto
{
    public int Id { get; init; }
    public string Name { get; init; } = string.Empty;
    public string LocationDisplay { get; init; } = string.Empty;
}