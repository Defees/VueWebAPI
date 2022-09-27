using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class RefreshToken : AuditableEntity
    {
        public RefreshToken(string userId, string token)
        {
            UserId = userId;
            Token = token;
        }
        public string UserId { get; set; }
        public string Token { get; set; }
    }
}
