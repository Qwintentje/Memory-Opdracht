﻿namespace Business;

public static class GameService
{
    public static Game? Game { get; set; }
    private static Stopwatch stopwatch { get; set; } = new Stopwatch();

    public static void Initialize(string? playerName)
    {
        Game = new Game(playerName, 5);
        Game.SetCards();
    }
    /*    public static void Initialize(string playerName, int cardAmount, plaatjes)
        {
    if(cardamount <5 )cardAmount = 5;
            game = new Game(playerName, cardAmount);
            game.SetCards(plaatjes);
        }*/

    public static void Start()
    {
        stopwatch.Reset();
        stopwatch.Start();
        Game.Status = GameStatus.InProgress;
    }

    public static bool IsValidCardChoice(int choice)
    {
        Card? card = Game?.Cards.Find(c => c.Index == choice);
        return choice >= 1 && choice <= (Game?.CardAmount * 2) && !card.IsTurned;
    }

    public static Card GetCard(int index)
    {
        Card? card = Game.Cards.Find(c => c.Index == index);
        if (card == null) throw new Exception("No card found");
        return card;
    }

    public static bool CheckMatch(Card card1, Card card2)
    {
        Game.Attempts++;
        if (card1.MatchingCard == card2 && card2.MatchingCard == card1)
        {
            return true;
        }
        Thread.Sleep(2000);
        card1.IsTurned = false;
        card2.IsTurned = false;
        return false;
    }

    public static bool CheckGameFinished()
    {
        bool isFinished = Game.Cards.All(c => c.IsTurned);
        if (isFinished)
        {
            EndGame();
        }
        return isFinished;
    }

    public static int CalculateScore()
    {
        int score = (int)(Math.Pow(2, Game.Cards.Count) / (Game.Duration * Game.Attempts)) * 1000;
        Game.Score = score;
        return score;
    }

    public static void EndGame()
    {
        Game.Status = GameStatus.Finished;
        stopwatch.Stop();
        Game.Duration = (int)stopwatch.Elapsed.TotalSeconds;
        CalculateScore();
    }
}
