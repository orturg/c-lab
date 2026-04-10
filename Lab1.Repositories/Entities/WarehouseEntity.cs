using SQLite;

namespace Lab1.Repositories.Entities;

[Table("Warehouses")]
public class WarehouseEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    [NotNull]
    public string Name { get; set; } = string.Empty;

    public int Location { get; set; }
}