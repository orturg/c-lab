using Lab1.Models;
using Lab1.Models.Enums;
using Lab1.Repositories.Entities;
using SQLite;

namespace Lab1.Repositories;

/// <summary>
/// Репозиторій для доступу до даних складів та товарів через sqlite
/// </summary>
public class SqliteWarehouseRepository(string dbPath) : IWarehouseRepository
{
    private readonly SQLiteAsyncConnection _db = new(dbPath,
        SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache);
    private bool _initialized;
    private readonly SemaphoreSlim _initLock = new(1, 1);

    private async Task EnsureInitializedAsync()
    {
        if (_initialized) return;
        await _initLock.WaitAsync();
        try
        {
            if (_initialized) return;
            await _db.CreateTableAsync<WarehouseEntity>();
            await _db.CreateTableAsync<ProductEntity>();
            await SeedIfEmptyAsync();
            _initialized = true;
        }
        finally
        {
            _initLock.Release();
        }
    }

    private async Task SeedIfEmptyAsync()
    {
        var count = await _db.Table<WarehouseEntity>().CountAsync();
        if (count > 0) return;

        var w1 = new WarehouseEntity { Name = "Головний", Location = (int)WarehouseLocation.Kyiv };
        var w2 = new WarehouseEntity { Name = "Склад Львів", Location = (int)WarehouseLocation.Lviv };
        var w3 = new WarehouseEntity { Name = "Склад Дніпро", Location = (int)WarehouseLocation.Dnipro };

        await _db.InsertAsync(w1);
        await _db.InsertAsync(w2);
        await _db.InsertAsync(w3);

        var products = new List<ProductEntity>
        {
            new() { WarehouseId = w1.Id, Name = "Ноутбук Lenovo IdeaPad 3", Quantity = 15, UnitPrice = 24999.00m, Category = (int)ProductCategory.Electronics, Description = "15.6\", Intel Core i5-1235U, 8 GB RAM, 512 GB SSD, Windows 11" },
            new() { WarehouseId = w1.Id, Name = "Смартфон Samsung Galaxy A54", Quantity = 40, UnitPrice = 12499.00m, Category = (int)ProductCategory.Electronics, Description = "6.4\", 128 GB, 8 GB RAM, Android 13, потрійна камера 50 МП" },
            new() { WarehouseId = w1.Id, Name = "Навушники Sony WH-1000XM5", Quantity = 25, UnitPrice = 9799.00m, Category = (int)ProductCategory.Electronics, Description = "Бездротові, активне шумопоглинання, 30 год роботи від батареї" },
            new() { WarehouseId = w1.Id, Name = "Джинси чоловічі Levi's 501", Quantity = 60, UnitPrice = 2199.00m, Category = (int)ProductCategory.Clothing, Description = "Класичний прямий крій, 100% бавовна, розміри 28–38" },
            new() { WarehouseId = w1.Id, Name = "Куртка зимова Columbia Whirlibird IV", Quantity = 30, UnitPrice = 5499.00m, Category = (int)ProductCategory.Clothing, Description = "Водонепроникна, утеплювач Omni-Heat, розміри S–XXL" },
            new() { WarehouseId = w1.Id, Name = "Кавоварка DeLonghi Magnifica Evo", Quantity = 10, UnitPrice = 18350.00m, Category = (int)ProductCategory.Electronics, Description = "Автоматична, вбудований кавомолок, 15 бар, 1.8 л резервуар" },
            new() { WarehouseId = w1.Id, Name = "Офісний стілець Comfort Pro", Quantity = 20, UnitPrice = 6800.00m, Category = (int)ProductCategory.Furniture, Description = "Ергономічний, сітчаста спинка, регулювання висоти та підлокітників" },
            new() { WarehouseId = w1.Id, Name = "Письмовий стіл UMK Loft", Quantity = 12, UnitPrice = 8200.00m, Category = (int)ProductCategory.Furniture, Description = "МДФ + метал, 140×60 см, колір венге, з кабель-менеджментом" },
            new() { WarehouseId = w1.Id, Name = "Шуруповерт Bosch GSR 18V-55", Quantity = 18, UnitPrice = 7490.00m, Category = (int)ProductCategory.Tools, Description = "Акумуляторний 18В, 55 Нм, 2 швидкості, у комплекті 2 акумулятори" },
            new() { WarehouseId = w2.Id, Name = "Планшет Apple iPad 10 Gen", Quantity = 8, UnitPrice = 17999.00m, Category = (int)ProductCategory.Electronics, Description = "10.9\", A14 Bionic, 64 GB, Wi-Fi, iPadOS 17" },
            new() { WarehouseId = w2.Id, Name = "Рюкзак міський Osprey Daylite 13L", Quantity = 35, UnitPrice = 2850.00m, Category = (int)ProductCategory.Clothing, Description = "13 л, поліестер 210D, відділення для ноутбука 13\", вага 430 г" },
            new() { WarehouseId = w2.Id, Name = "Дриль ударна Makita HP1631", Quantity = 5, UnitPrice = 4120.00m, Category = (int)ProductCategory.Tools, Description = "710 Вт, 13 мм патрон, 2800 об/хв, 0-45000 уд/хв, кейс у комплекті" },
        };

        await _db.InsertAllAsync(products);
    }

    public async Task<List<WarehouseModel>> GetAllWarehousesAsync()
    {
        await EnsureInitializedAsync();
        var entities = await _db.Table<WarehouseEntity>().ToListAsync();
        return entities.Select(ToModel).ToList();
    }

    public async Task<WarehouseModel?> GetWarehouseByIdAsync(int id)
    {
        await EnsureInitializedAsync();
        var entity = await _db.Table<WarehouseEntity>().FirstOrDefaultAsync(w => w.Id == id);
        return entity is null ? null : ToModel(entity);
    }

    public async Task<List<ProductModel>> GetProductsByWarehouseIdAsync(int warehouseId)
    {
        await EnsureInitializedAsync();
        var entities = await _db.Table<ProductEntity>()
            .Where(p => p.WarehouseId == warehouseId)
            .ToListAsync();
        return entities.Select(ToModel).ToList();
    }

    public async Task<ProductModel?> GetProductByIdAsync(int warehouseId, int productId)
    {
        await EnsureInitializedAsync();
        var entity = await _db.Table<ProductEntity>()
            .FirstOrDefaultAsync(p => p.Id == productId && p.WarehouseId == warehouseId);
        return entity is null ? null : ToModel(entity);
    }

    public async Task<WarehouseModel> AddWarehouseAsync(WarehouseModel warehouse)
    {
        await EnsureInitializedAsync();
        var entity = new WarehouseEntity { Name = warehouse.Name, Location = (int)warehouse.Location };
        await _db.InsertAsync(entity);
        return ToModel(entity);
    }

    public async Task UpdateWarehouseAsync(WarehouseModel warehouse)
    {
        await EnsureInitializedAsync();
        var entity = await _db.Table<WarehouseEntity>().FirstOrDefaultAsync(w => w.Id == warehouse.Id);
        if (entity is null) return;
        entity.Name = warehouse.Name;
        entity.Location = (int)warehouse.Location;
        await _db.UpdateAsync(entity);
    }

    public async Task DeleteWarehouseAsync(int id)
    {
        await EnsureInitializedAsync();
        await _db.Table<ProductEntity>().DeleteAsync(p => p.WarehouseId == id);
        await _db.Table<WarehouseEntity>().DeleteAsync(w => w.Id == id);
    }

    public async Task<ProductModel> AddProductAsync(ProductModel product)
    {
        await EnsureInitializedAsync();
        var entity = new ProductEntity
        {
            WarehouseId = product.WarehouseId,
            Name = product.Name,
            Quantity = product.Quantity,
            UnitPrice = product.UnitPrice,
            Category = (int)product.Category,
            Description = product.Description
        };
        await _db.InsertAsync(entity);
        return ToModel(entity);
    }

    public async Task UpdateProductAsync(ProductModel product)
    {
        await EnsureInitializedAsync();
        var entity = await _db.Table<ProductEntity>().FirstOrDefaultAsync(p => p.Id == product.Id);
        if (entity is null) return;
        entity.Name = product.Name;
        entity.Quantity = product.Quantity;
        entity.UnitPrice = product.UnitPrice;
        entity.Category = (int)product.Category;
        entity.Description = product.Description;
        await _db.UpdateAsync(entity);
    }

    public async Task DeleteProductAsync(int productId)
    {
        await EnsureInitializedAsync();
        await _db.Table<ProductEntity>().DeleteAsync(p => p.Id == productId);
    }

    private static WarehouseModel ToModel(WarehouseEntity e) =>
        new(e.Id, e.Name, (WarehouseLocation)e.Location);

    private static ProductModel ToModel(ProductEntity e) =>
        new(e.Id, e.WarehouseId, e.Name, e.Quantity, e.UnitPrice, (ProductCategory)e.Category, e.Description);
}
