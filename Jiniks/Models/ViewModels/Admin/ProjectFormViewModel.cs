using System.ComponentModel.DataAnnotations;

namespace Jiniks.Models.ViewModels.Admin;

public class ProjectFormViewModel
{
    public Guid? Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(5000)]
    public string Description { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string Location { get; set; } = string.Empty;

    public DateTimeOffset? CompletionDate { get; set; }

    [MaxLength(500)]
    public string Scope { get; set; } = string.Empty;

    public IFormFile? CoverImage { get; set; }
    public string? ExistingCoverImageUrl { get; set; }
    public bool IsActive { get; set; } = true;
}
