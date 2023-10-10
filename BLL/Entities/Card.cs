namespace Business.Entities;

public class Card
{
    public Guid Id { get; set; }
    public Card MatchingCard { get; set; }
    //public afbeelding
    public int Index { get; set; }
    public bool IsTurned { get; set; }

}
