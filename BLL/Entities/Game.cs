namespace Business.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public int Attempts { get; set; }
    public int CardAmount { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Game(string? playerName, int cardAmount)
    {
        PlayerName = string.IsNullOrWhiteSpace(playerName) ? "No Name Given" : playerName;
        CardAmount = cardAmount < 0 ? 2 : cardAmount;
    }

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


    public void PrintGame()
    {
        Console.Clear();
        if (Cards == null) throw new Exception("No cards available.");
        //Sort the cards so that they will be printend on index
        List<Card> sortedCards = Cards.OrderBy(card => card.Index).ToList();
        for (int i = 0; i < sortedCards.Count; i++)
        {
            if (sortedCards[i].IsTurned) Console.Write($"[{sortedCards[i].Symbol}] ");
            else Console.Write($"[{sortedCards[i].Index}] ");
            if ((i + 1) % CardAmount == 0) Console.WriteLine();
        }
    }
}
public enum Symbols
{

}
