using MySql.Data.MySqlClient;

namespace MySqlDatabase.Handlers
{
    public interface IMasterConnectionHandler
    {
        void Initialize();

        MySqlConnection GetConnection(string schema);
    }
}
