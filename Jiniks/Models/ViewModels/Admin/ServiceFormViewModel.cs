using System.ComponentModel.DataAnnotations;

namespace Jiniks.Models.ViewModels.Admin;

public class ServiceFormViewModel
{
    public Guid? Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(1000)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(100)]
    public string IconCssClass { get; set; } = string.Empty;

    public IFormFile? Image { get; set; }
    public string? ExistingImageUrl { get; set; }

    public int SortOrder { get; set; }
    public bool IsActive { get; set; } = true;
}
