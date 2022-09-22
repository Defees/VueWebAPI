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
    public class ChatDbContext : DbContext, IChatContext
    {
        public DbSet<Chat> Chats { get; set; }

        public ChatDbContext(DbContextOptions<ChatDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ChatConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
