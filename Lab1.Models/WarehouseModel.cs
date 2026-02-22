using Lab1.Models.Enums;
namespace Lab1.Models;

public class WarehouseModel(int id, string name, WarehouseLocation location)
{
    public int Id { get; } = id;
    public string Name { get; set; } = name;
    public WarehouseLocation Location { get; set; } = location;
}