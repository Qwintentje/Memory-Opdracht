ConsoleVisualization.PrintHighscores();
Console.WriteLine("Vul een naam in...");
string? playerName = Console.ReadLine();
if (!string.IsNullOrEmpty(playerName)) playerName = playerName.ToString(); else playerName = "Speler";
GameService.Initialize(playerName);
ConsoleVisualization.PrepareGame(playerName);


