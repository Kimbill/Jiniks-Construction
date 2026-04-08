using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class JobOpeningConfiguration : IEntityTypeConfiguration<JobOpening>
{
    public void Configure(EntityTypeBuilder<JobOpening> builder)
    {
        builder.HasKey(j => j.Id);
        builder.Property(j => j.Title).IsRequired().HasMaxLength(200);
        builder.Property(j => j.Location).IsRequired().HasMaxLength(300);
        builder.Property(j => j.Description).IsRequired().HasMaxLength(5000);

        builder.HasMany(j => j.Applications)
            .WithOne(a => a.JobOpening)
            .HasForeignKey(a => a.JobOpeningId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
