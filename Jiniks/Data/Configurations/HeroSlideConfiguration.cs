using Jiniks.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Jiniks.Data.Configurations;

public class HeroSlideConfiguration : IEntityTypeConfiguration<HeroSlide>
{
    public void Configure(EntityTypeBuilder<HeroSlide> builder)
    {
        builder.HasKey(h => h.Id);
        builder.Property(h => h.Headline).IsRequired().HasMaxLength(200);
        builder.Property(h => h.Subtext).IsRequired().HasMaxLength(500);
        builder.Property(h => h.CtaText).IsRequired().HasMaxLength(50);
        builder.Property(h => h.CtaUrl).IsRequired().HasMaxLength(500);
        builder.Property(h => h.BackgroundImageUrl).IsRequired().HasMaxLength(1000);
        builder.Property(h => h.BackgroundVideoUrl).HasMaxLength(1000);
    }
}
