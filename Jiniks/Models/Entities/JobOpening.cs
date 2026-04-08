using Jiniks.Models.Enums;

namespace Jiniks.Models.Entities;

public class JobOpening : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public JobType Type { get; set; }
    public string Description { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<JobApplication> Applications { get; set; } = [];
}
