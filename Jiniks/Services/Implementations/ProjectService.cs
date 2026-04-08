using Jiniks.Data;
using Jiniks.Models.Common;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class ProjectService : IProjectService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ProjectService> _logger;

    public ProjectService(ApplicationDbContext context, ILogger<ProjectService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Project>> GetActiveAsync()
    {
        return await _context.Projects
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CompletionDate)
            .ToListAsync();
    }

    public async Task<List<Project>> GetAllAsync()
    {
        return await _context.Projects
            .Include(p => p.Media)
            .OrderByDescending(p => p.CreatedAt)
            .ToListAsync();
    }

    public async Task<PaginatedList<Project>> GetAllPagedAsync(int page, int pageSize)
    {
        var query = _context.Projects
            .Include(p => p.Media)
            .OrderByDescending(p => p.CreatedAt);

        return await PaginatedList<Project>.CreateAsync(query, page, pageSize);
    }

    public async Task<PaginatedList<Project>> GetActivePagedAsync(int page, int pageSize)
    {
        var query = _context.Projects
            .Where(p => p.IsActive)
            .OrderByDescending(p => p.CompletionDate);

        return await PaginatedList<Project>.CreateAsync(query, page, pageSize);
    }

    public async Task<Project?> GetByIdAsync(Guid id)
    {
        return await _context.Projects.FindAsync(id);
    }

    public async Task<Project?> GetByIdWithMediaAsync(Guid id)
    {
        return await _context.Projects
            .Include(p => p.Media.OrderBy(m => m.SortOrder))
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    public async Task<Project> CreateAsync(Project project)
    {
        _context.Projects.Add(project);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created project {ProjectId} titled {Title}", project.Id, project.Title);
        return project;
    }

    public async Task<Project> UpdateAsync(Project project)
    {
        _context.Projects.Update(project);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated project {ProjectId}", project.Id);
        return project;
    }

    public async Task DeleteAsync(Guid id)
    {
        var project = await _context.Projects.Include(p => p.Media).FirstOrDefaultAsync(p => p.Id == id);
        if (project != null)
        {
            _context.Projects.Remove(project);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted project {ProjectId} and its {MediaCount} media items", id, project.Media.Count);
        }
    }

    public async Task<ProjectMedia> AddMediaAsync(ProjectMedia media)
    {
        _context.ProjectMedia.Add(media);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Added media {MediaId} to project {ProjectId}", media.Id, media.ProjectId);
        return media;
    }

    public async Task DeleteMediaAsync(Guid mediaId)
    {
        var media = await _context.ProjectMedia.FindAsync(mediaId);
        if (media != null)
        {
            _context.ProjectMedia.Remove(media);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted media {MediaId} from project {ProjectId}", mediaId, media.ProjectId);
        }
    }
}
