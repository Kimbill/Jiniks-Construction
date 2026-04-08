namespace Jiniks.Models.Entities;

public class JobApplication : BaseEntity
{
    public Guid JobOpeningId { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string CvUrl { get; set; } = string.Empty;
    public string? CvPublicId { get; set; }
    public bool IsRead { get; set; }

    public JobOpening JobOpening { get; set; } = null!;
}
