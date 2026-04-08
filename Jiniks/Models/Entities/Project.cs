namespace Jiniks.Models.Entities;

public class Project : BaseEntity
{
    public string Title { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
    public string Location { get; set; } = string.Empty;
    public DateTimeOffset? CompletionDate { get; set; }
    public string Scope { get; set; } = string.Empty;
    public string CoverImageUrl { get; set; } = string.Empty;
    public bool IsActive { get; set; } = true;

    public ICollection<ProjectMedia> Media { get; set; } = [];
}
