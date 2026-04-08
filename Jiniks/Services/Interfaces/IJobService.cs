using Jiniks.Models.Common;
using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IJobService
{
    Task<List<JobOpening>> GetActiveAsync();
    Task<List<JobOpening>> GetAllAsync();
    Task<PaginatedList<JobOpening>> GetAllPagedAsync(int page, int pageSize);
    Task<PaginatedList<JobOpening>> GetActivePagedAsync(int page, int pageSize);
    Task<PaginatedList<JobApplication>> GetApplicationsPagedAsync(Guid jobId, int page, int pageSize);
    Task<JobOpening?> GetByIdAsync(Guid id);
    Task<JobOpening?> GetByIdWithApplicationsAsync(Guid id);
    Task<JobOpening> CreateAsync(JobOpening job);
    Task<JobOpening> UpdateAsync(JobOpening job);
    Task DeleteAsync(Guid id);
    Task<JobApplication> SubmitApplicationAsync(JobApplication application);
    Task<List<JobApplication>> GetApplicationsAsync(Guid jobId);
    Task<JobApplication?> GetApplicationByIdAsync(Guid id);
    Task MarkApplicationAsReadAsync(Guid id);
    Task<int> GetUnreadApplicationCountAsync();
}
