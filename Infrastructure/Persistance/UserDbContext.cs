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
    public class UserDbContext : DbContext, IApplicationUserContext
    {
        public DbSet<User> ApplicationUsers { get; set; }

        public UserDbContext(DbContextOptions<UserDbContext> dbContextOptions)
            : base(dbContextOptions) { }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.ApplyConfiguration(new ApplicationUserConfiguration());
            base.OnModelCreating(builder);
        }
    }
}
