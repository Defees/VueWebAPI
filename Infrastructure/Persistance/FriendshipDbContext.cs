using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Persistance.Configurations;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance
{
    public class FriendshipDbContext : DbContext, IFriendshipContext
    {
        public DbSet<Friendship> Friendships { get; set; }

        public FriendshipDbContext(DbContextOptions<FriendshipDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new FriendshipConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
