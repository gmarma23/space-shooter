using System.Data.SQLite;

namespace SpaceShooter.src
{
    public class DatabaseManager
    {
        private const string connectionString = "Data source=SpaceShooter.db; Version=3";

        public void AddEntry(int score, string gameDuration)
        {
            createTable();
            string insertSQL = "Insert into Game(score, gameDuration) values (@score, @gameDuration)";
            SQLiteCommand command = new SQLiteCommand(insertSQL);
            command.Parameters.AddWithValue("score", score);
            command.Parameters.AddWithValue("gameDuration", gameDuration);
            executeNonQuerySQLiteCommand(command);
        }

        public List<(int, string)> GetTopEntries(int count)
        {
            createTable();
            List<(int, string)> topEntries = new List<(int, string)>();

            string selectSQL = "Select * from Game";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                SQLiteCommand command = new SQLiteCommand(selectSQL, connection);
                SQLiteDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    int score = reader.GetInt32(0);
                    string gameDuration = reader.GetString(1);

                    topEntries.Add((score, gameDuration));
                    topEntries = topEntries.OrderByDescending(entry => entry.Item1).ToList();
                    if (topEntries.Count == count + 1)
                        topEntries.RemoveAt(topEntries.Count - 1);
                }
            }

            return topEntries;
        }

        private void createTable()
        {
            const string createTable = "Create table if not exists Game(score integer, gameDuration Text)";
            executeNonQuerySQLiteCommand(new SQLiteCommand(createTable));
        }

        private void executeNonQuerySQLiteCommand(SQLiteCommand command)
        {
            SQLiteConnection connection = new SQLiteConnection(connectionString);
            connection.Open();
            command.Connection = connection;
            command.ExecuteNonQuery();
        }
    }
}
