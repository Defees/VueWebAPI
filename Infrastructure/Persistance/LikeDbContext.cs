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
    public class LikeDbContext : DbContext, ILikeContext
    {
        public DbSet<Like> Likes { get; set; }

        public LikeDbContext(DbContextOptions<LikeDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new LikeConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
