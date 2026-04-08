using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class ContactMessageConfiguration : IEntityTypeConfiguration<ContactMessage>
{
    public void Configure(EntityTypeBuilder<ContactMessage> builder)
    {
        builder.HasKey(c => c.Id);
        builder.Property(c => c.Name).IsRequired().HasMaxLength(150);
        builder.Property(c => c.Email).IsRequired().HasMaxLength(250);
        builder.Property(c => c.Phone).HasMaxLength(30);
        builder.Property(c => c.Subject).IsRequired().HasMaxLength(300);
        builder.Property(c => c.Message).IsRequired().HasMaxLength(5000);
    }
}
