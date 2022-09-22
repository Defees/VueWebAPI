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
    public class MessageDbContext : DbContext, IMessageContext
    {
        public DbSet<Message> Messages { get; set; }

        public MessageDbContext(DbContextOptions<MessageDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new MessageConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
