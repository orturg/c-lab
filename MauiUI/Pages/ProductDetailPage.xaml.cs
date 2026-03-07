using Lab1.Services;
using Lab1.ViewModels;

namespace MauiUI.Pages;

/// <summary>
/// Сторінка товару, відображаються всі дані про товар
/// </summary>
[QueryProperty(nameof(WarehouseId), "warehouseId")]
[QueryProperty(nameof(ProductId), "productId")]
public partial class ProductDetailPage : ContentPage
{
    private readonly IWarehouseService _warehouseService;
    private int _warehouseId;
    private int _productId;
    private bool _warehouseIdSet;
    private bool _productIdSet;

    public int WarehouseId
    {
        set
        {
            _warehouseId = value;
            _warehouseIdSet = true;
            TryLoadProduct();
        }
    }

    public int ProductId
    {
        set
        {
            _productId = value;
            _productIdSet = true;
            TryLoadProduct();
        }
    }

    public ProductDetailPage(IWarehouseService warehouseService)
    {
        InitializeComponent();
        _warehouseService = warehouseService;
    }

    private void TryLoadProduct()
    {
        if (!_warehouseIdSet || !_productIdSet)
            return;

        var products = _warehouseService.GetProductsByWarehouseId(_warehouseId);
        var product = products.FirstOrDefault(p => p.Id == _productId);

        if (product is null)
            return;

        PopulateFields(product);
    }

    private void PopulateFields(ProductViewModel product)
    {
        ProductNameLabel.Text = $"Товар #{product.Id}: {product.Name}";
        CategoryLabel.Text = product.CategoryDisplay;
        QuantityLabel.Text = $"{product.Quantity} шт.";
        UnitPriceLabel.Text = $"{product.UnitPrice:N2} грн";
        TotalValueLabel.Text = $"{product.TotalValue:N2} грн";
        DescriptionLabel.Text = product.Description;
    }
}