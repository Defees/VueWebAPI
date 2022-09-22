using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Entities
{
    public class Chat : BaseEntity
    {
        public List<Message> Messages { get; set; } = new List<Message>();
        public List<User> Users { get; set; } = new List<User>();
    }
}
