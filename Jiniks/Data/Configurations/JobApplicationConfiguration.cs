using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class JobApplicationConfiguration : IEntityTypeConfiguration<JobApplication>
{
    public void Configure(EntityTypeBuilder<JobApplication> builder)
    {
        builder.HasKey(a => a.Id);
        builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
        builder.Property(a => a.Email).IsRequired().HasMaxLength(250);
        builder.Property(a => a.CvUrl).IsRequired().HasMaxLength(1000);
        builder.Property(a => a.CvPublicId).HasMaxLength(500);
    }
}
