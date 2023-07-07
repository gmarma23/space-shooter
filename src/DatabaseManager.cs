using System.Data.SQLite;
using static SpaceShooter.utils.CustomExceptions;

namespace SpaceShooter
{
    public static class DatabaseManager
    {
        private const string connectionString = "Data source=SpaceShooter.db; Version=3";

        public static void AddHighscoresEntry(int score, string gameDuration)
        {
            createGameTable();
            string insertSQL = "Insert into Game(score, gameDuration) values (@score, @gameDuration)";

            using (SQLiteCommand command = new SQLiteCommand(insertSQL))
            {
                command.Parameters.AddWithValue("score", score);
                command.Parameters.AddWithValue("gameDuration", gameDuration);
                executeNonQuerySQLiteCommand(command);
            }
        }

        public static void AddOrUpdateOptionEntry(string name, bool value)
        {
            createOptionTable();

            string querySQL = "";
            try
            {
                _ = GetOptionValue(name);
                querySQL = "Update Option set value = @value where name = @name";
            }
            catch (EntryNotFoundException)
            {
                querySQL = "Insert into Option(name, value) values (@name, @value)";
            }

            using (SQLiteCommand command = new SQLiteCommand(querySQL))
            {
                command.Parameters.AddWithValue("name", name);
                command.Parameters.AddWithValue("value", value ? 1 : 0);
                executeNonQuerySQLiteCommand(command);
            }
        }

        public static List<(int, string)> GetTopHighscoresEntries(int count)
        {
            createGameTable();

            List<(int, string)> topEntries = new List<(int, string)>();

            string selectSQL = "Select * from Game";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectSQL, connection))
                {
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            int score = reader.GetInt32(0);
                            string gameDuration = reader.GetString(1);

                            topEntries.Add((score, gameDuration));
                            topEntries = topEntries.OrderByDescending(entry => entry.Item1).ThenBy(entry => entry.Item2).ToList();
                            if (topEntries.Count == count + 1)
                                topEntries.RemoveAt(topEntries.Count - 1);
                        }
                    }
                }
            }

            return topEntries;
        }

        public static bool GetOptionValue(string optionName)
        {
            createOptionTable();

            bool value = false;

            string selectSQL = "Select value from Option where name = @optionName";

            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                using (SQLiteCommand command = new SQLiteCommand(selectSQL, connection))
                {
                    command.Parameters.AddWithValue("optionName", optionName);
                    using (SQLiteDataReader reader = command.ExecuteReader())
                    {
                        if (reader.Read())
                            value = reader.GetInt32(0) == 1;
                        else
                            throw new EntryNotFoundException($"Option entry with name: \"{optionName}\" not found!");
                    }
                }
            }
                
            return value;
        }

        private static void createGameTable()
        {
            const string createTable = "Create table if not exists Game(score integer, gameDuration text)";

            using(SQLiteCommand command = new SQLiteCommand(createTable))
                executeNonQuerySQLiteCommand(command);   
        }

        private static void createOptionTable()
        {
            const string createTable = "Create table if not exists Option(name text, value integer)";

            using (SQLiteCommand command = new SQLiteCommand(createTable))
                executeNonQuerySQLiteCommand(command);
        }

        private static void executeNonQuerySQLiteCommand(SQLiteCommand command)
        {
            using (SQLiteConnection connection = new SQLiteConnection(connectionString))
            {
                connection.Open();
                command.Connection = connection;
                command.ExecuteNonQuery();
            }
        }
    }
}
