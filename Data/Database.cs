namespace Data;

/*
CREATE DATABASE IF NOT EXISTS Memory;

USE Memory;

CREATE TABLE IF NOT EXISTS `games` (
  `Id` varchar(500) NOT NULL,
  `PlayerName` varchar(45) NOT NULL,
  `Score` double NOT NULL,
  `Attempts` int NOT NULL,
  `CardAmount` int NOT NULL,
  `Duration` int NOT NULL,
  PRIMARY KEY(`Id`)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_general_ci;
*/
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
                using (MySqlCommand command = new MySqlCommand("INSERT INTO Games (Id, PlayerName, Score, Attempts, CardAmount, Duration) VALUES (@Id, @PlayerName, @Score, @Attempts, @CardAmount, @Duration)", connection))
                {
                    command.Parameters.AddWithValue("@Id", game.Id);
                    command.Parameters.AddWithValue("@PlayerName", game.PlayerName);
                    command.Parameters.AddWithValue("@Score", game.Score);
                    command.Parameters.AddWithValue("@Attempts", game.Attempts);
                    command.Parameters.AddWithValue("@CardAmount", game.CardAmount);
                    command.Parameters.AddWithValue("@Duration", game.Duration);
                    command.ExecuteNonQuery();
                }
            }
        }
        catch
        {
            throw new Exception("Er is iets fout met de database, neem contact op met Quinten");
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
                                CardAmount = reader.GetInt32("CardAmount"),
                                Duration = reader.GetInt32("Duration")
                            };
                            games.Add(game);
                        }
                    }
                }
            }
        }
        catch
        {
            throw new Exception("Er is iets fout met de database, neem contact op met Quinten");
        }
        return games;
    }
}
