using MySql.Data.MySqlClient;

namespace MySqlDatabase.Handlers
{
    public class MasterConnectionHandler : IMasterConnectionHandler
    {
        private MySqlConnection _masterConnection;
        private Dictionary<string, string> _connectionsStringDict;

        public MasterConnectionHandler()
        {
            Initialize();
        }

        public void Initialize()
        {
            try
            {
#if DEBUG
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringDebug"];
#else
                string connectionString = System.Configuration.ConfigurationManager.AppSettings["ConnectionStringProd"];
#endif

                if (connectionString == null)
                    throw new Exception("Connection string config missing in app.config");

                if (_masterConnection != null)
                    MySql.Disconnect(_masterConnection);

                _masterConnection = MySql.ConnectFromConnectionString(connectionString);
                _connectionsStringDict = new();
            }
            catch (Exception e)
            {
                throw;
            }
        }

        public MySqlConnection GetConnection(string schema)
        {
            try
            {
                if (!_connectionsStringDict.TryGetValue(schema, out string connectionString))
                {
                    using (MySqlDataReader reader = MySql.ExeQuery(_masterConnection, $"select value from connection where name = '{schema}'"))
                    {
                        if (!reader.HasRows)
                            throw new Exception("The schema does not exist");

                        reader.Read();

                        connectionString = reader.GetString(0);

                        _connectionsStringDict.Add(schema, connectionString);
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
