using Microsoft.Data.SqlClient;

namespace VulnerableApplication.Backend
{
    public interface IBackend
    {
        public List<ForumPost> GetForumPosts();
        public bool DeletePost(int id);
        public bool UpdatePost(int id, string message);
        public bool isUser(string email, string password);
        public bool isUserAdmin(string email);
        public bool CreatePost(string message, string username);
    }
}
