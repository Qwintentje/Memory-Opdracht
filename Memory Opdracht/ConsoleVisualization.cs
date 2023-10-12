namespace Memory_Opdracht;

public class ConsoleVisualization
{
    public static void PrepareGame(string? name)
    {

        while (true)
        {
            Console.Clear();
            PrintHighscores();
            Console.WriteLine($"Username: {name}\nDruk op spatie om het spel te starten...");
            ConsoleKeyInfo keyInfo = Console.ReadKey();

            if (keyInfo.Key == ConsoleKey.Spacebar)
            {
                Console.WriteLine("\nSpel aan het starten...");
                break; // Exit the loop once the space key is pressed
            }
        }
        GameService.Start();
        Start();
    }

    public static void Start()
    {
        while (true)
        {
            PrintGame();
            var firstChoice = GetChoice();
            Card firstCard = GameService.GetCard(firstChoice);
            firstCard.IsTurned = true;
            PrintGame();

            var secondChoice = GetChoice();
            Card secondCard = GameService.GetCard(secondChoice);
            secondCard.IsTurned = true;
            PrintGame();

            if (!GameService.CheckMatch(firstCard, secondCard))
            {
                firstCard.IsTurned = false;
                secondCard.IsTurned = false;
            }
            else
            {
                Console.WriteLine("Je hebt een match gevonden!");
                Thread.Sleep(2000);
            }
            if (GameService.CheckGameFinished())
            {
                Console.WriteLine("Game is afgelopen");
                if (GameService.Game != null)
                {
                    Console.WriteLine($"{GameService.Game.Attempts} pogingen in {GameService.Game.Duration} seconden");
                    Console.WriteLine($"Score: {GameService.Game.Score}");
                }
                else throw new Exception("Game not found");
                break;
            }
        }
    }
    public static int GetChoice()
    {
        int choice;

        while (true)
        {
            Console.WriteLine("Welke kaart wil je omdraaien...");

            string? userInput = Console.ReadLine();

            if (int.TryParse(userInput, out choice))
            {
                if (GameService.IsValidCardChoice(choice))
                {
                    break; // Exit the loop if a valid choice is given
                }
                else Console.WriteLine("Ongeldig nummer. Geef een geldig nummer die nog niet omgedraaid is");
            }
            else Console.WriteLine("Ongeldig nummer. Geef een geldig nummer die nog niet omgedraaid is");
        }
        return choice;
    }

    public static void PrintGame()
    {
        Console.Clear();
        if (GameService.Game?.Cards == null) throw new Exception("Geen kaarten beschikbaar.");
        //Sort the cards so that they will be printend based on index
        List<Card>? sortedCards = GameService.Game?.Cards.OrderBy(card => card.Index).ToList();
        for (int i = 0; i < sortedCards?.Count; i++)
        {
            if (sortedCards[i].IsTurned) Console.Write($"[{sortedCards[i].Symbol}] ");
            else Console.Write($"[{sortedCards[i].Index}] ");
            if ((i + 1) % GameService.Game?.CardAmount == 0) Console.WriteLine();
        }
    }

    public static void PrintHighscores()
    {
        Data.Database db = new Data.Database();
        var Highscores = db.GetHighscores();
        if (Highscores.Count > 0)
        {
            Console.WriteLine("Top 10 High Scores:");

            int playerNameWidth = 25;
            int scoreWidth = 10;
            int attemptsWidth = 15;
            int cardAmountWidth = 20;

            Console.WriteLine($"{FormatColumn("Speler", playerNameWidth)}{FormatColumn("Score", scoreWidth)}{FormatColumn("Pogingen", attemptsWidth)}{FormatColumn("Aantal kaarten", cardAmountWidth)}");
            Console.WriteLine("---------------------------------------------------------------");
            foreach (var game in Highscores)
            {
                Console.WriteLine($"{FormatColumn(game.PlayerName, playerNameWidth)}{FormatColumn(game.Score.ToString(), scoreWidth)}{FormatColumn(game.Attempts.ToString(), attemptsWidth)}{FormatColumn(game.CardAmount.ToString(), cardAmountWidth)}");
            }
            Console.WriteLine("---------------------------------------------------------------");
        }
    }

    private static string FormatColumn(string value, int width)
    {
        return value.PadRight(width);
    }

}
