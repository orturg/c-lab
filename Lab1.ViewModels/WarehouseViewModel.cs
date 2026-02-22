using Lab1.Models;
using Lab1.Models.Enums;

namespace Lab1.ViewModels;

/// <summary>
/// ViewModel для складу, відповідає за відображення, створення та редагування складу
/// </summary>
public class WarehouseViewModel
{
    private readonly WarehouseModel _model;
    public int Id => _model.Id;

    public string Name
    {
        get => _model.Name;
        set => _model.Name = value;
    }

    public WarehouseLocation Location
    {
        get => _model.Location;
        set => _model.Location = value;
    }

    public List<ProductViewModel> Products { get; private set; } = new();

    public bool ProductsLoaded { get; private set; } = false;

    public decimal TotalValue => Products.Sum(p => p.TotalValue);

    public WarehouseViewModel(WarehouseModel model)
    {
        _model = model;
    }

    public WarehouseViewModel(int id, string name, WarehouseLocation location)
        : this(new WarehouseModel(id, name, location))
    { }
    
    public void LoadProducts(IEnumerable<ProductViewModel> products)
    {
        Products = products.ToList();
        ProductsLoaded = true;
    }

    public WarehouseModel ToModel() => _model;

    public override string ToString()
        => $"[{Id}] {Name} | Місто: {LocationToUkrainian(Location)}";
    public string ToDetailedString()
    {
        return $"""
               СКЛАД #{Id}: {Name}
               Місцезнаходження : {LocationToUkrainian(Location)}
               Кількість товарів: {(ProductsLoaded ? Products.Count : "не завантажено")}
               Загальна вартість : {(ProductsLoaded ? $"{TotalValue:N2} грн" : "не завантажено")}
            """;
    }

    public static string LocationToUkrainian(WarehouseLocation location) => location switch
    {
        WarehouseLocation.Kyiv => "Київ",
        WarehouseLocation.Lviv => "Львів",
        WarehouseLocation.Odesa => "Одеса",
        WarehouseLocation.Kharkiv => "Харків",
        WarehouseLocation.Dnipro => "Дніпро",
        _ => location.ToString()
    };
}