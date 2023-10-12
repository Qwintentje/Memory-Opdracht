ConsoleVisualization.PrintHighscores();
Console.WriteLine("---------------------------------------------------------------");
Console.WriteLine("Vul een naam in...");
string? name = Console.ReadLine().ToString();
name = string.IsNullOrWhiteSpace(name) ? "Speler" : name;
GameService.Initialize(name);
ConsoleVisualization.PrepareGame(name);


