using Jiniks.Data;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class JobService : IJobService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<JobService> _logger;

    public JobService(ApplicationDbContext context, ILogger<JobService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<JobOpening>> GetActiveAsync()
    {
        return await _context.JobOpenings
            .Where(j => j.IsActive)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    public async Task<List<JobOpening>> GetAllAsync()
    {
        return await _context.JobOpenings
            .Include(j => j.Applications)
            .OrderByDescending(j => j.CreatedAt)
            .ToListAsync();
    }

    public async Task<JobOpening?> GetByIdAsync(Guid id)
    {
        return await _context.JobOpenings.FindAsync(id);
    }

    public async Task<JobOpening?> GetByIdWithApplicationsAsync(Guid id)
    {
        return await _context.JobOpenings
            .Include(j => j.Applications.OrderByDescending(a => a.CreatedAt))
            .FirstOrDefaultAsync(j => j.Id == id);
    }

    public async Task<JobOpening> CreateAsync(JobOpening job)
    {
        _context.JobOpenings.Add(job);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created job opening {JobId} titled {Title}", job.Id, job.Title);
        return job;
    }

    public async Task<JobOpening> UpdateAsync(JobOpening job)
    {
        _context.JobOpenings.Update(job);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated job opening {JobId}", job.Id);
        return job;
    }

    public async Task DeleteAsync(Guid id)
    {
        var job = await _context.JobOpenings.Include(j => j.Applications).FirstOrDefaultAsync(j => j.Id == id);
        if (job != null)
        {
            _context.JobOpenings.Remove(job);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted job opening {JobId}", id);
        }
    }

    public async Task<JobApplication> SubmitApplicationAsync(JobApplication application)
    {
        _context.JobApplications.Add(application);
        await _context.SaveChangesAsync();
        _logger.LogInformation("New job application from {Name} for job {JobId}", application.Name, application.JobOpeningId);
        return application;
    }

    public async Task<List<JobApplication>> GetApplicationsAsync(Guid jobId)
    {
        return await _context.JobApplications
            .Where(a => a.JobOpeningId == jobId)
            .OrderByDescending(a => a.CreatedAt)
            .ToListAsync();
    }

    public async Task<JobApplication?> GetApplicationByIdAsync(Guid id)
    {
        return await _context.JobApplications
            .Include(a => a.JobOpening)
            .FirstOrDefaultAsync(a => a.Id == id);
    }

    public async Task MarkApplicationAsReadAsync(Guid id)
    {
        var application = await _context.JobApplications.FindAsync(id);
        if (application != null)
        {
            application.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetUnreadApplicationCountAsync()
    {
        return await _context.JobApplications.CountAsync(a => !a.IsRead);
    }
}
