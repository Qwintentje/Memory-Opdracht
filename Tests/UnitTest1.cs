using BLL.Entities;
using Business;
using Business.Entities;

namespace Tests;

public class Tests
{
    [SetUp]
    public void Setup()
    {
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

    [Test]
    [TestCase(5, 10)]
    [TestCase(10, 20)]
    [TestCase(7, 14)]
    public void SetCardsShouldMakePairs(int pairs, int expectedMatches)
    {
        Game game = new Game("Quinten", pairs);

        game.SetCards();

        int matches = 0;
        List<string> symbols = new List<string>();
        List<string> imagePaths = new List<string>();
        foreach (var card in game.Cards)
        {
            if (!symbols.Contains(card.Symbol)) symbols.Add(card.Symbol);
            if (!imagePaths.Contains(card.imagePath)) imagePaths.Add(card.imagePath);
            foreach (var card2 in game.Cards)
            {
                if (card.MatchingCard == card2)
                {
                    matches++;
                }
            }
        }
        Assert.That(matches, Is.EqualTo(expectedMatches));
        Assert.That(symbols.Count(), Is.EqualTo(pairs));
        Assert.That(imagePaths.Count(), Is.EqualTo(pairs));
    }

    [Test]
    public void SetCardsShouldMakeUniqueIndexes()
    {
        Game game = new Game("Quinten", 5);

        game.SetCards();

        List<int> indexes = new List<int>();

        foreach (var card in game.Cards)
        {
            indexes.Add(card.Index);
        }

        Assert.That(indexes, Is.Unique);
    }

    [Test]
    [TestCase(1)]
    [TestCase(2)]
    [TestCase(3)]
    [TestCase(9)]
    public void GetCardShouldReturnCard(int index)
    {
        Game game = new Game("Quinten", 5);
        GameService.Game = game;
        game.SetCards();

        Card card = GameService.GetCard(index);

        Assert.That(card.Index, Is.EqualTo(index));
    }

    [Test]
    public void CheckMatch_ShouldReturnTrueWhenCardsMatch()
    {
        Card card = new Card(1);
        Card card2 = new Card(2);

        card.MatchingCard = card2;
        card2.MatchingCard = card;

        bool match = GameService.CheckMatch(card, card2);

        Assert.That(match, Is.True);
    }

    [Test]
    public void CheckMatch_ShouldReturnFalseWhenCardsDoNotMatch()
    {
        Card card = new Card(1);
        Card card2 = new Card(2);

        bool match = GameService.CheckMatch(card, card2);

        Assert.That(match, Is.False);
    }

    [Test]
    [TestCase(true)]
    [TestCase(false)]
    public void CheckGameFinishedShouldCheckAllCardsTurned(bool shouldFinish)
    {
        Game game = new Game("Quinten", 5);
        GameService.Game = game;
        game.SetCards();

        game.Cards.ForEach(card =>
        {
            card.IsTurned = shouldFinish;
        });

        bool isFinished = GameService.CheckGameFinished();

        Assert.That(isFinished, Is.EqualTo(shouldFinish));
        if (shouldFinish) Assert.That(game.Status, Is.EqualTo(GameStatus.Finished));
    }
}