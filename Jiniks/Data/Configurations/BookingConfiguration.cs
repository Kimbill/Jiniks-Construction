using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class BookingConfiguration : IEntityTypeConfiguration<Booking>
{
    public void Configure(EntityTypeBuilder<Booking> builder)
    {
        builder.HasKey(b => b.Id);
        builder.Property(b => b.Name).IsRequired().HasMaxLength(150);
        builder.Property(b => b.Email).IsRequired().HasMaxLength(250);
        builder.Property(b => b.Phone).IsRequired().HasMaxLength(30);
        builder.Property(b => b.ProjectType).IsRequired().HasMaxLength(200);
        builder.Property(b => b.Message).IsRequired().HasMaxLength(5000);
    }
}
