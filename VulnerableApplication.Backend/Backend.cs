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
                    .OrderByDescending(x => x.id)
                    .ToList();
            }
            catch { return []; }
        }

        public bool DeletePost(int id)
        {
            try
            {
                string sqlText = @"DELETE FROM Forum WHERE id = @id";
                var parameters = new List<SqlParameter> { new SqlParameter("@id", id), };
                RepoFunctions.ExecuteQuery(ConnectionString(), sqlText, parameters);
                return true;
            }
            catch { return false; }
        }

        public bool UpdatePost(int id, string message)
        {
            try
            {
                string sqlText = @"UPDATE Forum SET message = @message WHERE id = @id";
                var parameters = new List<SqlParameter> { new SqlParameter("@id", id), new SqlParameter("@message", message), };
                RepoFunctions.ExecuteQuery(ConnectionString(), sqlText, parameters);
                return true;
            }
            catch { return false; }
        }

        public bool CreatePost(string message, string username)
        {
            try
            {
                int userId = GetUserId(username);
                if (userId == -1) { throw new Exception(); }

                string sqlText = @"INSERT Forum (userId, message) VALUES (@userId, @message)";
                var parameters = new List<SqlParameter> { 
                    new SqlParameter("@userId", userId), 
                    new SqlParameter("@message", message), 
                };
                RepoFunctions.ExecuteQuery(ConnectionString(), sqlText, parameters);
                return true;
            }
            catch { return false; }
        }

        private int GetUserId(string email)
        {
            try
            {
                string query = "SELECT id, email, password, isAdmin FROM Users WHERE email = @email";
                var parameters = new List<SqlParameter>
                {
                    new SqlParameter("@email", email),
                };
                return RepoFunctions.RdExecuteQuery<User>(ConnectionString(), query, parameters).First().id;
            }
            catch { return -1; }
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
    }
}
