using Jiniks.Models.Enums;

namespace Jiniks.Models.Entities;

public class Booking : BaseEntity
{
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
    public string ProjectType { get; set; } = string.Empty;
    public BudgetRange BudgetRange { get; set; }
    public string Message { get; set; } = string.Empty;
    public bool IsRead { get; set; }
}
