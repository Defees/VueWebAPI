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
    public class PostDbContext : DbContext, IPostDbContext
    {
        public DbSet<Post> Posts { get; set ; }

        public PostDbContext(DbContextOptions<PostDbContext> dbContextOptions) 
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new PostConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
