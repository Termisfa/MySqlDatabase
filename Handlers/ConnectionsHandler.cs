using MySql.Data.MySqlClient;

namespace MySqlDatabase.Handlers
{
    public class ConnectionsHandler : IConnectionsHandler
    {
        private readonly MySqlConnection MasterConnection;
        private readonly Dictionary<string, string> ConnectionsStringDict;

        public ConnectionsHandler()
        {
            string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionString"];

            if (connectionString == null)
                throw new Exception("Connection string config missing in app.config");

            MasterConnection = MySql.ConnectFromConnectionString(connectionString);
            ConnectionsStringDict = new();
        }

        public void DeleteConnectionStrings()
        {
            ConnectionsStringDict?.Clear();
        }

        public MySqlConnection GetConnection(string schema)
        {
            try
            {
                if (!ConnectionsStringDict.TryGetValue(schema, out string connectionString))
                {
                    using (MySqlDataReader reader = MySql.ExeQuery(MasterConnection, $"select value from connections where name = '{schema}'"))
                    {
                        if (!reader.HasRows)
                            throw new Exception("The schema does not exist");

                        reader.Read();

                        connectionString = reader.GetString(0);

                        ConnectionsStringDict.Add(schema, connectionString);
                    }
                }

                MySqlConnection conn = MySql.ConnectFromConnectionString(connectionString);

                return conn;
            }
            catch (Exception e)
            {
                throw;
            }
        }

    }
}
