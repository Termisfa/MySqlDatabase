using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MySqlDatabase
{

    public static class MySql
    {
        public static MySqlConnection Connect(MySqlConnection databaseConnection)
        {
            try
            {
                databaseConnection.Open();

                return databaseConnection;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static MySqlConnection ConnectFromConnectionString(string connectionString)
        {
            try
            {
                MySqlConnection databaseConnection = GetConnection(connectionString);

                databaseConnection.Open();

                return databaseConnection;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static void Disconnect(MySqlConnection connection)
        {
            try
            {
                connection.Close();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static MySqlConnection GetConnection(string connectionString)
        {
            try
            {
                MySqlConnection databaseConnection = new MySqlConnection(connectionString);

                return databaseConnection;
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static MySqlDataReader ExeQuery(MySqlConnection connection, string query)
        {
            try
            {
                CheckForPossibleExploits(query);

                using (MySqlCommand commandDatabase = new(query, connection))
                {
                    commandDatabase.CommandTimeout = 60;

                    MySqlDataReader reader = commandDatabase.ExecuteReader();

                    return reader;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static Task<DbDataReader> ExeQueryAsync(MySqlConnection connection, string query)
        {
            try
            {
                CheckForPossibleExploits(query);

                using (MySqlCommand commandDatabase = new(query, connection))
                {
                    commandDatabase.CommandTimeout = 60;

                    Task<DbDataReader> reader = commandDatabase.ExecuteReaderAsync();

                    return reader;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static int ExeNonQuery(MySqlConnection connection, string query)
        {
            try
            {
                CheckForPossibleExploits(query);

                using (MySqlCommand commandDatabase = new(query, connection))
                {
                    commandDatabase.CommandTimeout = 60;

                    int affectedRows = commandDatabase.ExecuteNonQuery();

                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public static Task<int> ExeNonQueryAsync(MySqlConnection connection, string query)
        {
            try
            {
                CheckForPossibleExploits(query);

                using (MySqlCommand commandDatabase = new(query, connection))
                {
                    commandDatabase.CommandTimeout = 60;

                    Task<int> affectedRows = commandDatabase.ExecuteNonQueryAsync();

                    return affectedRows;
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }

        private static void CheckForPossibleExploits(string query)
        {
            if (query.Contains(';'))
                throw new Exception("The char ';' is not allowed to avoid exploits");
        }
    }
}