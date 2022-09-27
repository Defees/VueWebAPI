using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
    }
}
