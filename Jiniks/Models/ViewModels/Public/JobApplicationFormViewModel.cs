using System.ComponentModel.DataAnnotations;

namespace Jiniks.Models.ViewModels.Public;

public class JobApplicationFormViewModel
{
    public Guid JobOpeningId { get; set; }

    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(250)]
    public string Email { get; set; } = string.Empty;

    [Required]
    public IFormFile Cv { get; set; } = null!;
}
