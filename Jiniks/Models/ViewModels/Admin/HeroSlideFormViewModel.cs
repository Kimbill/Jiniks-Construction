using System.ComponentModel.DataAnnotations;
using Jiniks.Models.Enums;

namespace Jiniks.Models.ViewModels.Admin;

public class HeroSlideFormViewModel
{
    public Guid? Id { get; set; }

    [Required, MaxLength(200)]
    public string Headline { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string Subtext { get; set; } = string.Empty;

    [Required, MaxLength(50)]
    public string CtaText { get; set; } = string.Empty;

    [Required, MaxLength(500)]
    public string CtaUrl { get; set; } = string.Empty;

    public IFormFile? BackgroundImage { get; set; }
    public string? ExistingBackgroundImageUrl { get; set; }

    public MediaType BackgroundMediaType { get; set; } = MediaType.Image;
    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
