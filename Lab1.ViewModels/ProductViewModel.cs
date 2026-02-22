using Lab1.Models;
using Lab1.Models.Enums;

namespace Lab1.ViewModels;

/// <summary>
/// ViewModel для товару, відповідає за відображення, створення та редагування товару
/// </summary>
public class ProductViewModel
{
    private readonly ProductModel _model;
    public int Id => _model.Id;

    public int WarehouseId => _model.WarehouseId;

    public string Name
    {
        get => _model.Name;
        set => _model.Name = value;
    }

    public int Quantity
    {
        get => _model.Quantity;
        set => _model.Quantity = value;
    }

    public decimal UnitPrice
    {
        get => _model.UnitPrice;
        set => _model.UnitPrice = value;
    }

    public ProductCategory Category
    {
        get => _model.Category;
        set => _model.Category = value;
    }

    public string Description
    {
        get => _model.Description;
        set => _model.Description = value;
    }
    public decimal TotalValue => UnitPrice * Quantity;
    public ProductViewModel(ProductModel model)
    {
        _model = model;
    }

    public ProductViewModel(int id, int warehouseId, string name, int quantity,
        decimal unitPrice, ProductCategory category, string description)
        : this(new ProductModel(id, warehouseId, name, quantity, unitPrice, category, description))
    {
    }
    public ProductModel ToModel() => _model;

    public override string ToString()
        => $"[{Id}] {Name} | {CategoryToUkrainian(Category)} | {Quantity} шт. × {UnitPrice:N2} грн = {TotalValue:N2} грн";

    public string ToDetailedString()
    {
        return $"""
               ТОВАР #{Id}: {Name}
               Категорія        : {CategoryToUkrainian(Category)}
               Кількість        : {Quantity} шт.
               Ціна за одиницю  : {UnitPrice:N2} грн
               Загальна вартість: {TotalValue:N2} грн
               Опис             : {Description}
            """;
    }

    public static string CategoryToUkrainian(ProductCategory category) => category switch
    {
        ProductCategory.Electronics => "Електроніка",
        ProductCategory.Clothing => "Одяг",
        ProductCategory.Food => "Продукти",
        ProductCategory.Furniture => "Меблі",
        ProductCategory.Tools => "Інструменти",
        _ => category.ToString()
    };
}