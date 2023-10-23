using System.ComponentModel;

namespace Business.Entities;
public class Game : INotifyPropertyChanged
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
    private int attempts;

    public int Attempts
    {
        get { return attempts; }
        set
        {
            if (attempts != value)
            {
                attempts = value;
                OnPropertyChanged(nameof(Attempts));
            }
        }
    }
    public int CardAmount { get; set; }
    public List<Card> Cards { get; set; } = new List<Card>();
    public int Duration { get; set; }
    public GameStatus Status { get; set; } = GameStatus.Unknown;
    private List<int> usedImageIndexes = new List<int>();

    public event PropertyChangedEventHandler PropertyChanged;

    protected virtual void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    public Game(string? playerName, int cardAmount)
    {
        PlayerName = playerName == null ? "Speler" : playerName;
        CardAmount = cardAmount < 0 ? 4 : cardAmount;
        Status = GameStatus.Pending;
        Id = Guid.NewGuid().ToString();
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

                string path = GetRandomImagePath();
                card1.imagePath = path;
                card2.imagePath = path;
            }
        }
        Cards = Cards.OrderBy(card => card.Index).ToList();
    }

    public void SetCards(List<string> uploadedImages)
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
                string path = "";
                if (uploadedImages.Count > 0)
                {
                    path = uploadedImages.First();
                    uploadedImages.Remove(path);
                }
                else
                {
                    path = GetRandomImagePath();
                }
                card1.imagePath = path;
                card2.imagePath = path;



            }
        }
        Cards = Cards.OrderBy(card => card.Index).ToList();
    }

    private string GetRandomImagePath()
    {
        Random random = new Random();
        int index;
        do //this until the index is a random number between 1 and 15
        {
            index = random.Next(1, 16);
        } while (usedImageIndexes.Any(e => e == index) == true);
        usedImageIndexes.Add(index);
        return $"C:\\Users\\quint\\Documents\\Github\\Memory-Opdracht\\WPF\\assets\\icons\\Icon ({index}).png"; ;
    }

    private int GetRandomIndex()
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
