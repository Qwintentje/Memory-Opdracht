namespace Data;

public class GameDbModel //using a GameDbModel so that only needed data is being stores in the database
{
    public string Id { get; set; } = "";
    public string PlayerName { get; set; } = "";
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
    public int Duration { get; set; }
}
