using Microsoft.Data.SqlClient;
using System.Data;
using System.Reflection;

namespace VulnerableApplication.Backend
{
    public static class RepoFunctions
    {
        public static bool RowExists(string connectionString, string query, List<SqlParameter> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(connectionString))
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null) { command.Parameters.AddRange(parameters.ToArray()); }
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch { return false; }
        }

        public static List<T> ToList<T>(this SqlDataReader reader) where T : new()
        {
            List<T> result = new List<T>();
            while (reader.Read())
            {
                T item = new T();
                foreach (PropertyInfo property in typeof(T).GetProperties())
                {
                    if (HasColumn(reader, property.Name) && !reader.IsDBNull(reader.GetOrdinal(property.Name)))
                        property.SetValue(item, reader[property.Name]);
                }
                result.Add(item);
            }
            return result;
        }

        private static bool HasColumn(SqlDataReader reader, string columnName)
        {
            try { return reader.GetOrdinal(columnName) >= 0; }
            catch (IndexOutOfRangeException) { return false; }
        }

        public static List<T> RdExecuteQuery<T>(string connectionString, string sqlQuery, List<SqlParameter> sqlParameters) where T : new()
        {
            try
            {
                List<T> retVal = [];
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using SqlCommand command = new SqlCommand(sqlQuery, conn);
                if (sqlParameters != null) { command.Parameters.AddRange(sqlParameters.ToArray()); }

                using (SqlDataReader reader = command.ExecuteReader())
                {
                    retVal = reader.ToList<T>();
                }
                conn.Close();
                return retVal;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static void ExecuteQuery(string connectionString, string sqlQuery, List<SqlParameter> sqlParameters)
        {
            try
            {
                using SqlConnection conn = new SqlConnection(connectionString);
                conn.Open();
                using SqlCommand cmd = new SqlCommand(sqlQuery, conn);
                if (sqlParameters != null) { cmd.Parameters.AddRange(sqlParameters.ToArray()); }
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw;
            }
        }
    }
}
