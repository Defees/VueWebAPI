using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Like : AuditableEntity
    {
        public bool Enable { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string PostId { get; set; }
        public Post Post { get; set; }

    }
}
