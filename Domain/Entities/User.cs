using Microsoft.AspNetCore.Identity;

namespace Domain.Entities
{
    public class User : IdentityUser
    {
        public DateTime RegistrationDate { get; set; }
        public byte[]? ProfilePicture { get; set; } 
        public List<Friend> SentFriendRequests { get; set; } = new List<Friend>();
        public List<Friend> ReceievedFriendRequests { get; set; } = new List<Friend>();
        public List<Friend> Friends { get; set; } = new List<Friend>();

        public List<Post> Posts { get; set; } = new List<Post>();
        public List<Like> Likes { get; set; } = new List<Like>();
        public List<Chat> Chats { get; set; } = new List<Chat>();
        public List<Message> Messages { get; set; } = new List<Message>();
    }
}
