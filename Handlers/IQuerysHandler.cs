using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDatabase.Handlers
{
    public interface IQuerysHandler
    {
        Task<List<List<string>>> GetQueryResultAsync(string schema, string query);

        List<List<string>> GetQueryResult(string schema, string query);

        Task<int> GetNonQueryResultAsync(string schema, string query);

        void DeleteConnectionStrings();
    }
}
