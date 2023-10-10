namespace Business.Entities;

public class Game
{
    public Guid Id { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public int Attempts { get; set; }
    public int CardAmount { get; set; }
    public DateTime StartTime { get; set; }
    public DateTime EndTime { get; set; }

    public Game(string playerName, int cardAmount)
    {
        PlayerName = playerName;
        CardAmount = cardAmount;
    }
}
