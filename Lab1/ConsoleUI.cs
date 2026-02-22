namespace Lab1;

/// <summary>
/// Клас для роботи з консолʼю, відповідає за виведення та введення
/// </summary>
internal static class ConsoleUI
{
    public static void PrintHeader()
    {
        Console.Clear();
        Console.ForegroundColor = ConsoleColor.Cyan;
        Console.WriteLine("Старт програми");
        Console.ResetColor();
        Console.WriteLine();
    }
    public static void PrintSeparator()
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine(new string('─', 44));
        Console.ResetColor();
    }

    public static void WaitForEnter(string message = "Натисніть Enter для продовження")
    {
        Console.ForegroundColor = ConsoleColor.DarkGray;
        Console.WriteLine();
        Console.Write(message);
        Console.ResetColor();
        Console.ReadLine();
    }

    public static int ReadChoice(int min, int max)
    {
        while (true)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write($"Ваш вибір ({min}–{max}): ");
            Console.ResetColor();

            var input = Console.ReadLine();

            if (int.TryParse(input, out int choice) && choice >= min && choice <= max)
                return choice;

            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"Введіть число від {min} до {max}.");
            Console.ResetColor();
        }
    }

    public static void PrintSection(string title)
    {
        Console.ForegroundColor = ConsoleColor.Green;
        Console.WriteLine($"\n  {title}");
        Console.ResetColor();
        PrintSeparator();
    }

    public static void PrintError(string message)
    {
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine($"  ✗ {message}");
        Console.ResetColor();
    }
}