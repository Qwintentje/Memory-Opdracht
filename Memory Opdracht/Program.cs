ConsoleVisualization.PrintHighscores();
Console.WriteLine("Vul een naam in...");
string? playerName = Console.ReadLine();
if (!string.IsNullOrEmpty(playerName)) playerName = playerName.ToString(); else playerName = "Speler";
string name = GameService.Initialize(playerName);
ConsoleVisualization.PrepareGame(name);


