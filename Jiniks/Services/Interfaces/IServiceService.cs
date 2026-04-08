using Jiniks.Models.Common;
using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IServiceService
{
    Task<List<Service>> GetActiveAsync();
    Task<List<Service>> GetAllAsync();
    Task<PaginatedList<Service>> GetAllPagedAsync(int page, int pageSize);
    Task<Service?> GetByIdAsync(Guid id);
    Task<Service> CreateAsync(Service service);
    Task<Service> UpdateAsync(Service service);
    Task DeleteAsync(Guid id);
}
