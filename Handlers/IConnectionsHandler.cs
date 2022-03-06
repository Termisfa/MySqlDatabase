using MySql.Data.MySqlClient;

namespace MySqlDatabase.Handlers
{
    public interface IConnectionsHandler
    {
        void Initialize();

        MySqlConnection GetConnection(string schema);

        void DeleteConnectionStrings();
    }
}
