Console.WriteLine("Vul een naam in...");
string? name = Console.ReadLine()?.ToString();
GameService.Initialize(name);


while (true)
{
    Console.Clear();
    Console.WriteLine($"Username: {name}\nDruk op spatie om het spel te starten...");
    ConsoleKeyInfo keyInfo = Console.ReadKey();

    if (keyInfo.Key == ConsoleKey.Spacebar)
    {
        Console.WriteLine("\nSpel aan het starten...");
        GameService.StartConsole();
        break; // Exit the loop once the space key is pressed
    }
}
