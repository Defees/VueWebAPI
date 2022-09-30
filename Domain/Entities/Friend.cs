using Domain.Common;
using Domain.Enums;
using System.ComponentModel.DataAnnotations.Schema;

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
