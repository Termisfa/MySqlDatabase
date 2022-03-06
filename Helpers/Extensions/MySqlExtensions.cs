using MySql.Data.MySqlClient;
using System.Data.Common;

namespace MySqlDatabase.Helpers.Extensions
{
    public static class MySqlExtensions
    {
        public static List<List<string>> ParseToList(this DbDataReader reader)
        {
            try
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
            catch (Exception e)
            {
                throw;
            }
        }

        private static string GetStringOrNull(this DbDataReader reader, int colIndex)
        {
            try
            {
                if (!reader.IsDBNull(colIndex))
                    return reader.GetString(colIndex);
                return null;
            }
            catch (Exception e)
            {
                throw;
            }
        }
    }
}
