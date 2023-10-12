namespace Data;

public class GameDbModel
{
    public Guid Id { get; set; }
    public string PlayerName { get; set; }
    public int Score { get; set; }
    public int Attempts { get; set; }
    public int CardAmount { get; set; }

}
