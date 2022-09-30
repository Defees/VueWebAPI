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
    public class FriendshipConfiguration : IEntityTypeConfiguration<Friend>
    {
        public void Configure(EntityTypeBuilder<Friend> builder)
        {
            builder.ToTable("Friends");
            builder.HasKey(friend => friend.Id);
            builder.HasIndex(friend => friend.Id).IsUnique();
            builder.HasOne(friend => friend.RequestedTo)
                    .WithMany(user => user.ReceievedFriendRequests)
                    .HasForeignKey(friend => friend.RequestedToId)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
            builder.HasOne(friend => friend.RequestedBy)
                    .WithMany(user => user.SentFriendRequests)
                    .HasForeignKey(friend => friend.RequestedById)
                    .OnDelete(DeleteBehavior.Restrict)
                    .IsRequired();
        }
    }
}
