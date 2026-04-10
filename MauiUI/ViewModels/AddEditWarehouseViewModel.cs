using System.Windows.Input;
using Lab1.Models.Enums;
using Lab1.Services;

namespace MauiUI.ViewModels;

/// <summary>
/// VM для сторінки створення та редагування складу
/// </summary>
public class AddEditWarehouseViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IWarehouseService _warehouseService;
    private int? _warehouseId;

    public bool IsEditMode => _warehouseId.HasValue;
    public string PageTitle => IsEditMode ? "Редагування скаду" : "Новий склад";

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set => SetProperty(ref _name, value);
    }

    private int _selectedLocationIndex;
    public int SelectedLocationIndex
    {
        get => _selectedLocationIndex;
        set => SetProperty(ref _selectedLocationIndex, value);
    }

    public List<string> LocationOptions { get; } = ["Київ", "Львів", "Одеса", "Харків", "Дніпро"];

    public ICommand LoadDataCommand { get; }
    public ICommand SaveCommand { get; }
    public ICommand CancelCommand { get; }

    public AddEditWarehouseViewModel(IWarehouseService warehouseService)
    {
        _warehouseService = warehouseService;
        LoadDataCommand = new Command(async () => await LoadDataAsync());
        SaveCommand = new Command(async () => await SaveAsync());
        CancelCommand = new Command(async () => await Shell.Current.GoToAsync(".."));
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (query.TryGetValue("warehouseId", out var value) &&
            int.TryParse(value?.ToString(), out var id))
            _warehouseId = id;
        else
            _warehouseId = null;

        OnPropertyChanged(nameof(IsEditMode));
        OnPropertyChanged(nameof(PageTitle));
    }

    public async Task LoadDataAsync()
    {
        if (!IsEditMode) return;
        IsBusy = true;
        try
        {
            var form = await _warehouseService.GetWarehouseFormAsync(_warehouseId!.Value);
            if (form is null) return;
            Name = form.Name;
            SelectedLocationIndex = (int)form.Location;
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
            await Shell.Current.DisplayAlert("Помилка", "Введіть назву складу", "OK");
            return;
        }

        IsBusy = true;
        try
        {
            var location = (WarehouseLocation)SelectedLocationIndex;
            if (IsEditMode)
                await _warehouseService.UpdateWarehouseAsync(_warehouseId!.Value, Name.Trim(), location);
            else
                await _warehouseService.AddWarehouseAsync(Name.Trim(), location);

            await Shell.Current.GoToAsync("..");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
