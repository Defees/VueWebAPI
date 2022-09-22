using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistance.Configurations
{
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friendship>
    {
        public void Configure(EntityTypeBuilder<Friendship> builder)
        {
            builder.ToTable("Friendship");
            builder.HasKey(friendship => friendship.Id);
            builder.HasIndex(friendship => friendship.Id).IsUnique();
            builder.HasAlternateKey(friendship => new { friendship.User1, friendship.User2});
        }
    }
}
