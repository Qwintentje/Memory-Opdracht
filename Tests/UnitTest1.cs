using Business.Entities;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
        //Test name and amount validation
        //Test if amount makes cards
        //Test if cards are pairs
        //Test all functions in gameservice
        //Maybe test db connection store and get
    }


    [Test]
    [TestCase(null, 0, "Speler", 5)]
    [TestCase("Quinten", 5, "Quinten", 5)]
    public void GameInputValidation(string name, int amount, string expectedName, int expectedAmount)
    {
        Game game = new Game(name, amount);
        Assert.That(expectedName, Is.EqualTo(game.PlayerName));
        Assert.That(expectedAmount, Is.EqualTo(game.CardAmount));
    }
}