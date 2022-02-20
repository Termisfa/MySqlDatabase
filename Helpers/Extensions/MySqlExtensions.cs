using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MySqlDatabase.Helpers.Extensions
{
    public static class MySqlExtensions
    {
        public static string GetStringOrNull(this MySqlDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }
        public static List<List<string>> ParseToList(this DbDataReader reader)
        {
            List<List<string>> result = new();

            if (reader.HasRows)
            {
                while (reader.Read())
                {
                    List<string> rowList = new();
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        rowList.Add(reader.GetStringOrNull(i));
                    }
                    result.Add(rowList);
                }
            }

            return result;
        }

        public static string GetStringOrNull(this DbDataReader reader, int colIndex)
        {
            if (!reader.IsDBNull(colIndex))
                return reader.GetString(colIndex);
            return null;
        }
    }
}
