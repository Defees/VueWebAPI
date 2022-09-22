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
    public class MessageConfiguration : IEntityTypeConfiguration<Message>
    {
        public void Configure(EntityTypeBuilder<Message> builder)
        {
            builder.ToTable("Messages");
            builder.HasKey(message => message.Id);
            builder.HasIndex(message => message.Id).IsUnique();
            builder.Property(message => message.MessageBody).HasMaxLength(500);
            builder.Property(message => message.DateCreated).HasDefaultValue(DateTime.UtcNow);
        }
    }
}
