﻿using Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class Post : BaseEntity
    {
        public string PostBody { get; set; }
        public DateTime DateCreated { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }
        public List<Like> Likes { get; set; } = new List<Like>();

    }
}