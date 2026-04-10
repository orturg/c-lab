using SQLite;

namespace Lab1.Repositories.Entities;

[Table("Products")]
public class ProductEntity
{
    [PrimaryKey, AutoIncrement]
    public int Id { get; set; }

    public int WarehouseId { get; set; }

    [NotNull]
    public string Name { get; set; } = string.Empty;

    public int Quantity { get; set; }

    public decimal UnitPrice { get; set; }

    public int Category { get; set; }

    public string Description { get; set; } = string.Empty;
}