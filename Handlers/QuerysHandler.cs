using MySql.Data.MySqlClient;
using MySqlDatabase.Helpers.Extensions;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Common;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MySqlDatabase.Handlers
{
    public class QuerysHandler : IQuerysHandler
    {
        private readonly IConnectionsHandler _cacheConnections;

        public QuerysHandler(IConnectionsHandler cacheConnections)
        {
            _cacheConnections = cacheConnections;
        }


        public async Task<List<List<string>>> GetQueryResultAsync(string schema, string query)
        {
            MySqlConnection mySqlConnection = default;

            try
            {
                mySqlConnection = _cacheConnections.GetConnection(schema);

                List<List<string>> result = new();

                using (DbDataReader reader = await MySql.ExeQueryAsync(mySqlConnection, query))
                {
                    result = reader.ParseToList();
                }

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (mySqlConnection != default)
                    MySql.Disconnect(mySqlConnection);
            }
        }

        public List<List<string>> GetQueryResult(string schema, string query)
        {
            MySqlConnection mySqlConnection = default;

            try
            {
                mySqlConnection = _cacheConnections.GetConnection(schema);

                List<List<string>> result = new();

                using (MySqlDataReader reader = MySql.ExeQuery(mySqlConnection, query))
                {
                    result = reader.ParseToList();
                }

                return result;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (mySqlConnection != default)
                    MySql.Disconnect(mySqlConnection);
            }
        }


        public async Task<int> GetNonQueryResultAsync(string schema, string query)
        {
            MySqlConnection mySqlConnection = default;

            try
            {
                mySqlConnection = _cacheConnections.GetConnection(schema);

                int affectedRows = await MySql.ExeNonQueryAsync(mySqlConnection, query);

                return affectedRows;
            }
            catch (Exception e)
            {
                throw;
            }
            finally
            {
                if (mySqlConnection != default)
                    MySql.Disconnect(mySqlConnection);
            }
        }

        public void DeleteConnectionStrings()
        {
            _cacheConnections.DeleteConnectionStrings();
        }
    }
}
