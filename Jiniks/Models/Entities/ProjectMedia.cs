using Jiniks.Models.Enums;

namespace Jiniks.Models.Entities;

public class ProjectMedia : BaseEntity
{
    public Guid ProjectId { get; set; }
    public string MediaUrl { get; set; } = string.Empty;
    public string? PublicId { get; set; }
    public MediaType MediaType { get; set; }
    public int SortOrder { get; set; }

    public Project Project { get; set; } = null!;
}
