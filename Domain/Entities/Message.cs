﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Message : AuditableEntity
    {
        public string MessageBody { get; set; }
        public string UserId { get; set; }
        public User User { get; set; }
        public string ChatId { get; set; }
        public Chat Chat { get; set; }
    }
}
