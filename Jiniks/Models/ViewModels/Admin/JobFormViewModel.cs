using System.ComponentModel.DataAnnotations;
using Jiniks.Models.Enums;

namespace Jiniks.Models.ViewModels.Admin;

public class JobFormViewModel
{
    public Guid? Id { get; set; }

    [Required, MaxLength(200)]
    public string Title { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string Location { get; set; } = string.Empty;

    [Required]
    public JobType Type { get; set; }

    [Required, MaxLength(5000)]
    public string Description { get; set; } = string.Empty;

    public bool IsActive { get; set; } = true;
}
