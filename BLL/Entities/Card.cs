namespace Business.Entities;
public class Card
{
    public string Id { get; set; }
    public Card? MatchingCard { get; set; }
    public int Index { get; set; }
    public string Symbol { get; set; } = "";
    //public afbeelding
    public bool IsTurned { get; set; }
    public string imagePath { get; set; } = "";

    public Card(int i)
    {
        Id = Guid.NewGuid().ToString();
        Symbol = GetSymbol(i);
    }

    private string GetSymbol(int i)
    {
        switch (i)
        {
            case 0: return "!";
            case 2: return "@";
            case 4: return "#";
            case 6: return "$";
            case 8: return "%";
            case 10: return "^";
            case 12: return "&";
            case 14: return "*";
            case 16: return "(";
            case 18: return ")";
            case 20: return "-";
            case 22: return "_";
            case 24: return "=";
            case 26: return "+";
            case 28: return "}";
            case 30: return "{";
            case 32: return ";";
            case 34: return "/";
            case 36: return "<";
            case 38: return ">";
            case 40: return "";
            case 42: return "}";
            case 44: return "}";
            default: return "?";
        }
    }
}
