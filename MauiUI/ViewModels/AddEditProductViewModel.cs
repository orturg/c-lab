using System.Windows.Input;
using Lab1.Models.Enums;
using Lab1.Services;

namespace MauiUI.ViewModels;

/// <summary>
/// VM для сторінки створення та редагування товару
/// </summary>
public class AddEditProductViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IWarehouseService _warehouseService;
    private int _warehouseId;
    private int? _productId;

    public bool IsEditMode => _productId.HasValue;
    public string PageTitle => IsEditMode ? "Редагування товару" : "Новий товар";

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private string _quantityText = string.Empty;
    public string QuantityText
    {
        get => _quantityText;
        set => SetProperty(ref _quantityText, value);
    }

    private string _priceText = string.Empty;
    public string PriceText
    {
        get => _priceText;
        set => SetProperty(ref _priceText, value);
    }

    private string _description = string.Empty;
    public string Description
    {
        get => _description;
        set => SetProperty(ref _description, value);
    }

    private int _selectedCategoryIndex;
    public int SelectedCategoryIndex
    {
        get => _selectedCategoryIndex;
        set => SetProperty(ref _selectedCategoryIndex, value);
    }

    public List<string> CategoryOptions { get; } =
        ["Електроніка", "Одяг", "Продукти", "Меблі", "Інструменти"];

    public ICommand LoadDataCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddEditProductViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
        LoadDataCommand = new Command(async () => await LoadDataAsync());
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var wVal))
            int.TryParse(wVal?.ToString(), out _warehouseId);

        if (query.TryGetValue("productId", out var pVal) &&
            int.TryParse(pVal?.ToString(), out var pid))
            _productId = pid;
        else
            _productId = null;

        OnPropertyChanged(nameof(IsEditMode));
        OnPropertyChanged(nameof(PageTitle));
    }

    public async Task LoadDataAsync()
    {
        if (!IsEditMode) return;
        IsBusy = true;
        try
        {
            var form = await _warehouseService.GetProductFormAsync(_warehouseId, _productId!.Value);
            if (form is null) return;
            Name = form.Name;
            QuantityText = form.Quantity.ToString();
            PriceText = form.UnitPrice.ToString("F2");
            Description = form.Description;
            SelectedCategoryIndex = (int)form.Category;
        }
        finally
        {
            IsBusy = false;
        }
    }

    private async Task SaveAsync()
    {
        if (string.IsNullOrWhiteSpace(Name))
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть назву товару", "OK");
            return;
        }

        if (!int.TryParse(QuantityText, out var quantity) || quantity < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть корекну кількість", "OK");
            return;
        }

        if (!decimal.TryParse(
                PriceText.Replace(',', '.'),
                System.Globalization.NumberStyles.Any,
                System.Globalization.CultureInfo.InvariantCulture,
                out var price) || price < 0)
        {
            await Shell.Current.DisplayAlert("Помилка", "Введіть коректну ціну", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var category = (ProductCategory)SelectedCategoryIndex;
            if (IsEditMode)
                await _warehouseService.UpdateProductAsync(
                    _warehouseId, _productId!.Value, Name.Trim(), quantity, price, category, Description.Trim());
            else
                await _warehouseService.AddProductAsync(
                    _warehouseId, Name.Trim(), quantity, price, category, Description.Trim());

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
