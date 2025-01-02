namespace VulnerableApplication.Backend
{
    public interface IBackend
    {
        public bool isUser(string email, string password);
        public string RemoveDomainFromEmail(string email);
        public bool isUserAdmin(string email);
        public List<ForumPost> GetForumPosts();
    }
}
