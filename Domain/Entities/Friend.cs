using Domain.Common;
using Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Friend : Entity
    {
        public DateTime? RequestTime { get; set; }
        public DateTime? BecameFriendsTime { get; set; }
        public FriendRequestFlag FriendRequestFlag { get; set; }
        public string RequestedById { get; set; }
        public User RequestedBy { get; set; }
        public string RequestedToId { get; set; }
        public User RequestedTo { get; set; }

        [NotMapped]
        public bool Approved => FriendRequestFlag == FriendRequestFlag.Approved;

    }
}
