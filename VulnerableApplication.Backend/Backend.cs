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

        public List<ForumPost> GetForumPosts()
        {
            try
            {
                string sqlText = @"SELECT f.[id]
                                         ,u.[email]
                                         ,u.[isAdmin]
                                         ,f.[message]
                                        FROM Forum f
                                        JOIN Users u on f.[userId] = u.[id]";
                return RepoFunctions.RdExecuteQuery<ForumPost>(ConnectionString(), sqlText, null)
                    .Select(x => new ForumPost() { id = x.id, message = x.message, isAdmin = x.isAdmin, email = RemoveDomainFromEmail(x.email) }).ToList();
            }
            catch { return []; }
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
                return RepoFunctions.RowExists(ConnectionString(), query, parameters);
            }
            catch { return false; }
        }

        public bool isUserAdmin(string email)
        {
            try
            {
                string query = "SELECT 1 FROM Users WHERE email = @email AND isAdmin = 1";
                var parameters = new List<SqlParameter> { new SqlParameter("@email", email), };
                return RepoFunctions.RowExists(ConnectionString(), query, parameters);
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
