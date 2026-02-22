using Lab1;
using Lab1.Services;
using Lab1.ViewModels;

var service = new WarehouseService();

ConsoleUI.PrintHeader();
Console.WriteLine("Завантаження даних");

var warehouses = service.GetAllWarehouses();

Console.ForegroundColor = ConsoleColor.Green;
Console.WriteLine($"Завантажено {warehouses.Count} склади.\n");
Console.ResetColor();

ConsoleUI.WaitForEnter("Натисніть Enter для переходу до головного меню");

bool running = true;

while (running)
{
    Console.Clear();
    ConsoleUI.PrintHeader();
    ConsoleUI.PrintSection("Склади");

    for (int i = 0; i < warehouses.Count; i++)
    {
        Console.WriteLine($"{i + 1}. {warehouses[i]}");
    }

    Console.WriteLine();
    Console.WriteLine($"{warehouses.Count + 1}. Оновити список складів");
    Console.WriteLine($"0. Вийти з програми");
    Console.WriteLine();

    int choice = ConsoleUI.ReadChoice(0, warehouses.Count + 1);

    if (choice == 0)
    {
        running = false;
    }
    else if (choice == warehouses.Count + 1)
    {
        warehouses = service.GetAllWarehouses();
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\nСписок складів оновлено.");
        Console.ResetColor();
        ConsoleUI.WaitForEnter();
    }
    else
    {
        var selectedWarehouse = warehouses[choice - 1];
        ShowWarehouseDetails(selectedWarehouse, service);
    }
}

Console.Clear();
ConsoleUI.PrintHeader();
Console.ForegroundColor = ConsoleColor.Cyan;
Console.WriteLine("програма завердшена");
Console.ResetColor();
Console.WriteLine();

static void ShowWarehouseDetails(WarehouseViewModel warehouse, WarehouseService service)
{
    service.LoadProductsForWarehouse(warehouse);

    bool inWarehouse = true;

    while (inWarehouse)
    {
        Console.Clear();
        ConsoleUI.PrintHeader();

        Console.WriteLine(warehouse.ToDetailedString());
        Console.WriteLine();
        ConsoleUI.PrintSection($"ТОВАРИ СКЛАДУ «{warehouse.Name}»");

        if (warehouse.Products.Count == 0)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Цей склад пустий");
            Console.ResetColor();
        }
        else
        {
            for (int i = 0; i < warehouse.Products.Count; i++)
            {
                Console.WriteLine($"{i + 1}. {warehouse.Products[i]}");
            }
        }

        Console.WriteLine();
        Console.WriteLine($"{warehouse.Products.Count + 1}. Деталі товару");
        Console.WriteLine($"0. Повернутись до списку складів");
        Console.WriteLine();

        int choice = ConsoleUI.ReadChoice(0, warehouse.Products.Count + 1);

        if (choice == 0)
        {
            inWarehouse = false;
        }
        else if (choice == warehouse.Products.Count + 1)
        {
            if (warehouse.Products.Count == 0)
            {
                ConsoleUI.PrintError("Склад пустий");
                ConsoleUI.WaitForEnter();
            }
            else
            {
                ShowProductSelection(warehouse, service);
            }
        }
    }
}

static void ShowProductSelection(WarehouseViewModel warehouse, WarehouseService service)
{
    Console.Clear();
    ConsoleUI.PrintHeader();
    ConsoleUI.PrintSection($"оберіть товар — склад «{warehouse.Name}»");

    for (int i = 0; i < warehouse.Products.Count; i++)
    {
        Console.WriteLine($"{i + 1}. [{warehouse.Products[i].Id}] {warehouse.Products[i].Name}");
    }

    Console.WriteLine($"0. Скасувати");
    Console.WriteLine();

    int productChoice = ConsoleUI.ReadChoice(0, warehouse.Products.Count);

    if (productChoice == 0)
        return;

    var selectedProduct = warehouse.Products[productChoice - 1];

    Console.Clear();
    ConsoleUI.PrintHeader();
    Console.WriteLine(selectedProduct.ToDetailedString());
    ConsoleUI.WaitForEnter();
}