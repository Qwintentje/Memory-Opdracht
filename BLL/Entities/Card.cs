namespace Business.Entities;

public class Card
{
    public string Id { get; set; }
    public Card? MatchingCard { get; set; }
    public int Index { get; set; }
    public string Symbol { get; set; } = "";
    //public afbeelding
    public bool IsTurned { get; set; }

    public Card(int i)
    {
        Symbol = GetSymbol(i);
        Id = Guid.NewGuid().ToString();
    }

    private string GetSymbol(int i)
    {
        // You can customize this logic based on your symbol requirements
        switch (i % 10)
        {
            case 0: return "?";
            case 1: return "!";
            case 2: return "#";
            case 3: return "$";
            case 4: return "%";
            case 5: return "&";
            case 6: return "@";
            case 7: return "*";
            case 8: return "+";
            case 9: return "-";
            default: return "?";
        }
    }
}
