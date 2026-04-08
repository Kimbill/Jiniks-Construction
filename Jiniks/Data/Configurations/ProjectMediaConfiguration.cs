using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class ProjectMediaConfiguration : IEntityTypeConfiguration<ProjectMedia>
{
    public void Configure(EntityTypeBuilder<ProjectMedia> builder)
    {
        builder.HasKey(m => m.Id);
        builder.Property(m => m.MediaUrl).IsRequired().HasMaxLength(1000);
        builder.Property(m => m.PublicId).HasMaxLength(500);
    }
}
