namespace Business.Entities;
public class Game
{
    public string Id { get; set; }
    public string PlayerName { get; set; }
    private int score;
    public double Score
    {
        get => score;
        set
        {
            score = (int)value;
        }
    }
    public int Attempts { get; set; }
    public int CardAmount { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public int Duration { get; set; }
    public GameStatus Status { get; set; } = GameStatus.Unknown;
    public Game(string? playerName, int cardAmount)
    {
        PlayerName = playerName;
        CardAmount = cardAmount < 0 ? 2 : cardAmount;
        Status = GameStatus.Pending;
        Id = Guid.NewGuid().ToString();
    }

    /*    public Game(Guid id, string playerName, double score, int attempts, int cardAmount)
        {
            Id = id;
            PlayerName = playerName;
            Score = score;
            Attempts = attempts;
            CardAmount = cardAmount;
        }*/

    public void SetCards()
    {
        Cards = new List<Card>(CardAmount * 2);
        for (int i = 0; i < CardAmount * 2; i++)
        {
            if (i % 2 == 0) //For each set of 2 cards make them a pair
            {
                Card card1 = new Card(i);
                Card card2 = new Card(i);

                card1.MatchingCard = card2; //Set the 2 cards to a pair
                card2.MatchingCard = card1;

                card1.Index = GetRandomIndex();
                Cards.Add(card1);

                card2.Index = GetRandomIndex();
                Cards.Add(card2);
            }
        }
    }

    public int GetRandomIndex()
    {
        Random random = new Random();
        int index;
        do //this until the index is a random number between 1 and the amount of cards
        {
            index = random.Next(1, CardAmount * 2 + 1);
        } while (Cards?.Any(c => c.Index == index) == true);
        return index;
    }



}
