using Microsoft.Data.SqlClient;

namespace VulnerableApplication.Backend
{
    public interface IBackend
    {
        public List<ForumPost> GetForumPosts();
        public bool DeletePost(int id);
        public bool UpdatePost(int id, string message);
        public bool CreatePost(string message, string username);
        public bool DeleteUser(int id);
        public bool UpdateUser(int id, string password);
        public bool ToggleUserAdmin(int id, bool isCurrentAdmin);
        public bool CreateUser(string email, string password);
        public List<User> GetUsers();
        public bool isUser(string email, string password);
        public bool isUserAdmin(string email);
    }
}
