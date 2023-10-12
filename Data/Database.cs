namespace Data;

public class Database
{
    private string connectionString = "Server=localhost;Database=Memory;User ID=root;";

    public void InsertGame(GameDbModel game)
    {
        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Games (Id, PlayerName, Score, Attempts, CardAmount) VALUES (@Id, @PlayerName, @Score, @Attempts, @CardAmount)", connection))
                {
                    command.Parameters.AddWithValue("@Id", game.Id);
                    command.Parameters.AddWithValue("@PlayerName", game.PlayerName);
                    command.Parameters.AddWithValue("@Score", game.Score);
                    command.Parameters.AddWithValue("@Attempts", game.Attempts);
                    command.Parameters.AddWithValue("@CardAmount", game.CardAmount);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch
        {
            Console.WriteLine("Er is iets fout met de database, neem contact op met Quinten");
        }
    }
    public List<GameDbModel> GetHighscores()
    {
        List<GameDbModel> games = new List<GameDbModel>();

        try
        {
            using (MySqlConnection connection = new MySqlConnection(connectionString))
            {
                connection.Open();

                using (MySqlCommand command = new MySqlCommand("SELECT * FROM Games ORDER BY Score DESC LIMIT 10", connection))
                {
                    using (MySqlDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            GameDbModel game = new GameDbModel
                            {
                                Id = reader.GetString("Id"),
                                PlayerName = reader.GetString("PlayerName"),
                                Score = reader.GetInt32("Score"),
                                Attempts = reader.GetInt32("Attempts"),
                                CardAmount = reader.GetInt32("CardAmount")
                            };
                            games.Add(game);
                        }
                    }
                }
            }
        }
        catch
        {
            Console.WriteLine("Er is iets fout met de database, neem contact op met Quinten");
        }
        return games;
    }
}
