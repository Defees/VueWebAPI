using Domain.Common;

namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public string MessageBody { get; set; }
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
