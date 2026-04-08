using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IProjectService
{
    Task<List<Project>> GetActiveAsync();
    Task<List<Project>> GetAllAsync();
    Task<Project?> GetByIdAsync(Guid id);
    Task<Project?> GetByIdWithMediaAsync(Guid id);
    Task<Project> CreateAsync(Project project);
    Task<Project> UpdateAsync(Project project);
    Task DeleteAsync(Guid id);
    Task<ProjectMedia> AddMediaAsync(ProjectMedia media);
    Task DeleteMediaAsync(Guid mediaId);
}
