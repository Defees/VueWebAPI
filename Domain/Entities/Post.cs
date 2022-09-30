using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : AuditableEntity
    {
        public string PostBody { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public List<Like> Likes { get; set; } = new List<Like>();

    }
}
