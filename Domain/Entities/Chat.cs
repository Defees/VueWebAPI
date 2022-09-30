using Domain.Common;


namespace Domain.Entities
{
    public class Chat : Entity
    {
        public string? ChatName { get; set; }
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
