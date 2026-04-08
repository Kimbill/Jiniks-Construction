using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Admin;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class JobsController : Controller
{
    private readonly IJobService _jobService;
    private readonly ILogger<JobsController> _logger;

    public JobsController(IJobService jobService, ILogger<JobsController> logger)
    {
        _jobService = jobService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var jobs = await _jobService.GetAllAsync();
        return View("~/Views/Admin/Jobs/Index.cshtml", jobs);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Jobs/Create.cshtml", new JobFormViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(JobFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Jobs/Create.cshtml", model);

        var job = new JobOpening
        {
            Title = model.Title,
            Location = model.Location,
            Type = model.Type,
            Description = model.Description,
            IsActive = model.IsActive
        };

        await _jobService.CreateAsync(job);
        TempData["Success"] = "Job opening created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var job = await _jobService.GetByIdAsync(id);
        if (job == null) return NotFound();

        var model = new JobFormViewModel
        {
            Id = job.Id,
            Title = job.Title,
            Location = job.Location,
            Type = job.Type,
            Description = job.Description,
            IsActive = job.IsActive
        };

        return View("~/Views/Admin/Jobs/Edit.cshtml", model);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, JobFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Jobs/Edit.cshtml", model);

        var job = await _jobService.GetByIdAsync(id);
        if (job == null) return NotFound();

        job.Title = model.Title;
        job.Location = model.Location;
        job.Type = model.Type;
        job.Description = model.Description;
        job.IsActive = model.IsActive;

        await _jobService.UpdateAsync(job);
        TempData["Success"] = "Job opening updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _jobService.DeleteAsync(id);
        TempData["Success"] = "Job opening deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Applications/{jobId}")]
    public async Task<IActionResult> Applications(Guid jobId)
    {
        var job = await _jobService.GetByIdWithApplicationsAsync(jobId);
        if (job == null) return NotFound();

        return View("~/Views/Admin/Jobs/Applications.cshtml", job);
    }

    [HttpPost("Applications/MarkRead/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> MarkApplicationRead(Guid id)
    {
        var application = await _jobService.GetApplicationByIdAsync(id);
        if (application == null) return NotFound();

        await _jobService.MarkApplicationAsReadAsync(id);
        return RedirectToAction(nameof(Applications), new { jobId = application.JobOpeningId });
    }
}
