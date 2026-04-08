using Jiniks.Data;
using Jiniks.Models.Common;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class ServiceService : IServiceService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ServiceService> _logger;

    public ServiceService(ApplicationDbContext context, ILogger<ServiceService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Service>> GetActiveAsync()
    {
        return await _context.Services
            .Where(s => s.IsActive)
            .OrderBy(s => s.SortOrder)
            .ToListAsync();
    }

    public async Task<List<Service>> GetAllAsync()
    {
        return await _context.Services
            .OrderBy(s => s.SortOrder)
            .ToListAsync();
    }

    public async Task<PaginatedList<Service>> GetAllPagedAsync(int page, int pageSize)
    {
        var query = _context.Services
            .OrderBy(s => s.SortOrder);

        return await PaginatedList<Service>.CreateAsync(query, page, pageSize);
    }

    public async Task<Service?> GetByIdAsync(Guid id)
    {
        return await _context.Services.FindAsync(id);
    }

    public async Task<Service> CreateAsync(Service service)
    {
        _context.Services.Add(service);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created service {ServiceId} titled {Title}", service.Id, service.Title);
        return service;
    }

    public async Task<Service> UpdateAsync(Service service)
    {
        _context.Services.Update(service);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated service {ServiceId}", service.Id);
        return service;
    }

    public async Task DeleteAsync(Guid id)
    {
        var service = await _context.Services.FindAsync(id);
        if (service != null)
        {
            _context.Services.Remove(service);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted service {ServiceId}", id);
        }
    }
}
