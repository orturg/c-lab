using Lab1.Models.Enums;

namespace Lab1.Services.DTO;

/// <summary>
/// DTO для форми створення або редагування складу
/// </summary>
public class WarehouseFormDto
{
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public WarehouseLocation Location { get; set; }
}