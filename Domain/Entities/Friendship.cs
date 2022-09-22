using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Friendship : BaseEntity
    {
        public DateTime DateConfirmation { get; set; }
        public bool Confirmed { get; set; }

        public User User1 { get; set; }
        public User User2 { get; set; }

    }
}
