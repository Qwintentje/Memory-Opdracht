using System.Diagnostics;

namespace Business;

public static class GameService
{
    private static Game? game { get; set; }
    private static Stopwatch stopwatch { get; set; } = new Stopwatch();

    public static void Initialize(string? playerName)
    {
        game = new Game(playerName, 5);
        game.SetCards();
    }
    /*    public static void Initialize(string playerName, int cardAmount, plaatjes)
        {
    if(cardamount <5 )cardAmount = 5;
            game = new Game(playerName, cardAmount);
            game.SetCards(plaatjes);
        }*/

    public static void StartConsole()
    {
        stopwatch.Reset();
        stopwatch.Start();
        game?.PrintGame();

        int choice = GetChoice();
        Card? card = game?.Cards.Find(c => c.Index == choice);
        card.IsTurned = true;
        game?.PrintGame();
        int choice2 = GetChoice();
        Card? card2 = game?.Cards.Find(c => c.Index == choice2);
        card2.IsTurned = true;
        game?.PrintGame();
    }

    public static int GetChoice()
    {
        int choice;

        while (true)
        {
            Console.WriteLine("Welke kaart wil je omdraaien...");

            // Read the user input as a string
            string userInput = Console.ReadLine();

            // Try to parse the string to an integer
            if (int.TryParse(userInput, out choice))
            {
                // Now 'choice' holds the parsed integer
                // Add your logic for checking if the choice is valid
                if (IsValidCardChoice(choice))
                {
                    break; // Exit the loop if a valid choice is given
                }
                else
                {
                    Console.WriteLine("Invalid choice. Please enter a valid number.");
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Please enter a valid number.");
            }
        }

        return choice;
    }

    public static bool IsValidCardChoice(int choice)
    {
        // Add your logic for checking if the choice is within the valid range or meets any other criteria
        // For example, you might check if it's within the range of the number of cards
        return choice >= 1 && choice <= (game?.CardAmount * 2);
    }

}
