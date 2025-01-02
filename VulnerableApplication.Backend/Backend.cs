using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace VulnerableApplication.Backend
{
    public class Backend : IBackend
    {
        private IConfiguration config { get; set; }

        public Backend(IConfiguration _config)
        {
            config = _config;
        }

        private string ConnectionString() => config.GetConnectionString("SoftwareSecurityFinalProject");

        private bool RowExists(string query, List<SqlParameter> parameters)
        {
            try
            {
                using (var connection = new SqlConnection(ConnectionString()))
                using (var command = new SqlCommand(query, connection))
                {
                    if (parameters != null) { command.Parameters.AddRange(parameters.ToArray()); }
                    connection.Open();
                    return command.ExecuteScalar() != null;
                }
            }
            catch { return false; }
        }

        public bool isUser(string email, string password)
        {
            try
            {
                string query = "SELECT 1 FROM Users WHERE email = @email AND password = @password";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@email", email),
                    new SqlParameter("@password", password),
                };
                return RowExists(query, parameters);
            }
            catch { return false; }
        }

        public string RemoveDomainFromEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return string.Empty;

            int atIndex = email.IndexOf('@');
            return atIndex > 0 ? email.Substring(0, atIndex) : email;
        }
    }
}
