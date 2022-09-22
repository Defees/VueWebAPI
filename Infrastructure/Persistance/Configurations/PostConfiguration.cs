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
    public class PostConfiguration : IEntityTypeConfiguration<Post>
    {
        public void Configure(EntityTypeBuilder<Post> builder)
        {
            builder.ToTable("Posts");
            builder.HasKey(post => post.Id);
            builder.HasIndex(post => post.Id).IsUnique();
            builder.Property(post => post.PostBody).HasMaxLength(500);
            builder.Property(post => post.DateCreated).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
