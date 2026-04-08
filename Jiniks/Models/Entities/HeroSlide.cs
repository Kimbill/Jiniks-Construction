using Jiniks.Models.Enums;

namespace Jiniks.Models.Entities;

public class HeroSlide : BaseEntity
{
    public string Headline { get; set; } = string.Empty;
    public string Subtext { get; set; } = string.Empty;
    public string CtaText { get; set; } = string.Empty;
    public string CtaUrl { get; set; } = string.Empty;
    public string BackgroundImageUrl { get; set; } = string.Empty;
    public string? BackgroundVideoUrl { get; set; }
    public MediaType BackgroundMediaType { get; set; } = MediaType.Image;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
