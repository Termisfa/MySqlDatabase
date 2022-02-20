using MySql.Data.MySqlClient;

namespace MySqlDatabase.Handlers
{
    public interface IConnectionsHandler
    {
        MySqlConnection GetConnection(string schema);

        void DeleteConnectionStrings();
    }
}
