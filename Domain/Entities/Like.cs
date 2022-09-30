using Domain.Common;

namespace Domain.Entities
{
    public class Like : AuditableEntity
    {
        public bool Enable { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }

    }
}
