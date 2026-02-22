using Lab1.Models;
using Lab1.Models.Enums;

namespace Lab1.Services;

/// <summary>
/// Клас штучного зберігання, що імітує базу даних під час розробки та тестування
/// </summary>
internal static class FakeStorage
{
    internal static readonly List<WarehouseModel> Warehouses;
    internal static readonly List<ProductModel> Products;
    
    static FakeStorage()
    {
        Warehouses = InitializeWarehouses();
        Products = InitializeProducts();
    }

    private static List<WarehouseModel> InitializeWarehouses()
    {
        return
        [
            new WarehouseModel(1, "Головний", WarehouseLocation.Kyiv),
            new WarehouseModel(2, "Склад Львів", WarehouseLocation.Lviv),
            new WarehouseModel(3, "Склад Дніпро", WarehouseLocation.Dnipro)
        ];
    }

    private static List<ProductModel> InitializeProducts()
    {
        return
        [
            new ProductModel(
                id: 1,
                warehouseId: 1,
                name: "Ноутбук Lenovo IdeaPad 3",
                quantity: 15,
                unitPrice: 24_999.00m,
                category: ProductCategory.Electronics,
                description: "15.6\", Intel Core i5-1235U, 8 GB RAM, 512 GB SSD, Windows 11"
            ),

            new ProductModel(
                id: 2, 
                warehouseId: 1,
                name: "Смартфон Samsung Galaxy A54",
                quantity: 40,
                unitPrice: 12_499.00m,
                category: ProductCategory.Electronics,
                description: "6.4\", 128 GB, 8 GB RAM, Android 13, потрійна камера 50 МП"
            ),

            new ProductModel(
                id: 3, 
                warehouseId: 1,
                name: "Навушники Sony WH-1000XM5",
                quantity: 25,
                unitPrice: 9_799.00m,
                category: ProductCategory.Electronics,
                description: "Бездротові, активне шумопоглинання, 30 год роботи від батареї"
            ),

            new ProductModel(
                id: 4,
                warehouseId: 1,
                name: "Джинси чоловічі Levi's 501",
                quantity: 60,
                unitPrice: 2_199.00m,
                category: ProductCategory.Clothing,
                description: "Класичний прямий крій, 100% бавовна, розміри 28–38"
            ),

            new ProductModel(
                id: 5, 
                warehouseId: 1,
                name: "Куртка зимова Columbia Whirlibird IV",
                quantity: 30,
                unitPrice: 5_499.00m,
                category: ProductCategory.Clothing,
                description: "Водонепроникна, утеплювач Omni-Heat, розміри S–XXL"
            ),

            new ProductModel(
                id: 6, 
                warehouseId: 1,
                name: "Кавоварка DeLonghi Magnifica Evo",
                quantity: 10,
                unitPrice: 18_350.00m,
                category: ProductCategory.Electronics,
                description: "Автоматична, вбудований кавомолок, 15 бар, 1.8 л резервуар"
            ),

            new ProductModel(
                id: 7, 
                warehouseId: 1,
                name: "Офісний стілець Comfort Pro",
                quantity: 20,
                unitPrice: 6_800.00m,
                category: ProductCategory.Furniture,
                description: "Ергономічний, сітчаста спинка, регулювання висоти та підлокітників"
            ),

            new ProductModel(
                id: 8,
                warehouseId: 1,
                name: "Письмовий стіл UMK Loft",
                quantity: 12,
                unitPrice: 8_200.00m,
                category: ProductCategory.Furniture,
                description: "МДФ + метал, 140×60 см, колір венге, з кабель-менеджментом"
            ),

            new ProductModel(
                id: 9,
                warehouseId: 1,
                name: "Шуруповерт Bosch GSR 18V-55",
                quantity: 18,
                unitPrice: 7_490.00m,
                category: ProductCategory.Tools,
                description: "Акумуляторний 18В, 55 Нм, 2 швидкості, у комплекті 2 акумулятори"
            ),
            
            
            new ProductModel(
                id: 10,
                warehouseId: 2,
                name: "Планшет Apple iPad 10 Gen",
                quantity: 8, 
                unitPrice: 17_999.00m,
                category: ProductCategory.Electronics,
                description: "10.9\", A14 Bionic, 64 GB, Wi-Fi, iPadOS 17"
            ),

            new ProductModel(
                id: 11,
                warehouseId: 2,
                name: "Рюкзак міський Osprey Daylite 13L",
                quantity: 35,
                unitPrice: 2_850.00m,
                category: ProductCategory.Clothing,
                description: "13 л, поліестер 210D, відділення для ноутбука 13\", вага 430 г"
            ),

            new ProductModel(
                id: 12,
                warehouseId: 2,
                name: "Дриль ударна Makita HP1631",
                quantity: 5, 
                unitPrice: 4_120.00m,
                category: ProductCategory.Tools,
                description: "710 Вт, 13 мм патрон, 2800 об/хв, 0-45000 уд/хв, кейс у комплекті"
            )
        ];
    }
}