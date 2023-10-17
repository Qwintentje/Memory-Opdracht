namespace Business;

public static class GameService
{
    public static Game? Game { get; set; }
    private static Stopwatch stopwatch { get; set; } = new Stopwatch();
    private static int defaultCardAmount = 5;

    public static string Initialize()
    {
        Console.WriteLine("Vul een naam in...");
        string? playerName = Console.ReadLine();
        if (!string.IsNullOrEmpty(playerName)) playerName = playerName.ToString(); else playerName = "Speler";
        Game = new Game(playerName, defaultCardAmount);
        Game.SetCards();
        return playerName;
    }
    /*    public static void Initialize(string playerName, int cardAmount, plaatjes)
        {
    if(cardamount <5 )cardAmount = 5;
            game = new Game(playerName, cardAmount);
            game.SetCards(plaatjes);
        }*/

    public static void Start()
    {
        stopwatch.Reset();
        stopwatch.Start();
        if (Game != null) Game.Status = GameStatus.InProgress;
    }

    public static bool IsValidCardChoice(int choice)
    {
        Card? card = Game?.Cards.Find(c => c.Index == choice);
        if (card != null) return choice >= 1 && choice <= (Game?.CardAmount * 2) && !card.IsTurned; else return true;
    }

    public static Card GetCard(int index)
    {
        if (Game != null)
        {
            Card? card = Game.Cards.Find(c => c.Index == index);
            if (card == null) throw new Exception("No card found");
            return card;
        }
        else throw new Exception("Game not found");
    }

    public static bool CheckMatch(Card card1, Card card2)
    {
        if (Game != null) Game.Attempts++;
        if (card1.MatchingCard == card2 && card2.MatchingCard == card1)
        {
            return true;
        }
        Thread.Sleep(2000);
        card1.IsTurned = false;
        card2.IsTurned = false;
        return false;
    }

    public static bool CheckGameFinished()
    {
        if (Game != null)
        {
            bool isFinished = Game.Cards.All(c => c.IsTurned);
            if (isFinished)
            {
                EndGame();
            }
            return isFinished;
        }
        else throw new Exception("Game not found");

    }

    public static double CalculateScore()
    {
        if (Game != null)
        {
            double first = (Game.CardAmount * 2) * (Game.CardAmount * 2);
            double second = Game.Duration * Game.Attempts;
            double score = (first / second) * 1000;
            Game.Score = score;
            return score;
        }
        else throw new Exception("Game not found");
    }

    public static void EndGame()
    {
        if (Game != null)
        {
            Game.Status = GameStatus.Finished;
            stopwatch.Stop();
            Game.Duration = (int)stopwatch.Elapsed.TotalSeconds;
            CalculateScore();
            Database db = new Database();
            GameDbModel gameDbModel = new GameDbModel()
            {
                Id = Game.Id,
                PlayerName = Game.PlayerName,
                Score = Game.Score,
                CardAmount = Game.CardAmount,
                Attempts = Game.Attempts,
                Duration = Game.Duration
            };
            db.InsertGame(gameDbModel);
        }
        else throw new Exception("Game not found");
    }
}
