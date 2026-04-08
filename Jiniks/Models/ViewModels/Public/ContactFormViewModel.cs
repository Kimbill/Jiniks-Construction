using System.ComponentModel.DataAnnotations;

namespace Jiniks.Models.ViewModels.Public;

public class ContactFormViewModel
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(250)]
    public string Email { get; set; } = string.Empty;

    [Phone, MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required, MaxLength(300)]
    public string Subject { get; set; } = string.Empty;

    [Required, MaxLength(5000)]
    public string Message { get; set; } = string.Empty;
}
