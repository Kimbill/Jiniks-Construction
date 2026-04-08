using System.ComponentModel.DataAnnotations;
using Jiniks.Models.Enums;

namespace Jiniks.Models.ViewModels.Public;

public class BookingFormViewModel
{
    [Required, MaxLength(150)]
    public string Name { get; set; } = string.Empty;

    [Required, EmailAddress, MaxLength(250)]
    public string Email { get; set; } = string.Empty;

    [Required, Phone, MaxLength(30)]
    public string Phone { get; set; } = string.Empty;

    [Required, MaxLength(200)]
    public string ProjectType { get; set; } = string.Empty;

    [Required]
    public BudgetRange BudgetRange { get; set; }

    [Required, MaxLength(5000)]
    public string Message { get; set; } = string.Empty;
}
