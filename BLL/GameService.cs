using Business.Entities;

namespace Business;

public class GameService
{
    private Game game { get; set; }

    public void Initialize(string playerName, int cardAmount)
    {
        game = new Game(playerName, cardAmount);
    }
}
