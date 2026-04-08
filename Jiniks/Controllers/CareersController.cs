using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Public;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers;

public class CareersController : Controller
{
    private readonly IJobService _jobService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<CareersController> _logger;

    public CareersController(
        IJobService jobService,
        ICloudinaryService cloudinaryService,
        ILogger<CareersController> logger)
    {
        _jobService = jobService;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var jobs = await _jobService.GetActivePagedAsync(page, 10);
        return View(jobs);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var job = await _jobService.GetByIdAsync(id);
        if (job == null || !job.IsActive)
        {
            _logger.LogWarning("Job opening {JobId} not found or inactive", id);
            return NotFound();
        }

        return View(job);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Apply(JobApplicationFormViewModel model)
    {
        if (!ModelState.IsValid)
        {
            var job = await _jobService.GetByIdAsync(model.JobOpeningId);
            if (job == null) return NotFound();
            return View("Details", job);
        }

        var uploadResult = await _cloudinaryService.UploadRawFileAsync(model.Cv, "cvs");

        var application = new JobApplication
        {
            JobOpeningId = model.JobOpeningId,
            Name = model.Name,
            Email = model.Email,
            CvUrl = uploadResult.Url,
            CvPublicId = uploadResult.PublicId
        };

        await _jobService.SubmitApplicationAsync(application);
        _logger.LogInformation("Job application submitted by {Name} for job {JobId}", model.Name, model.JobOpeningId);

        TempData["Success"] = "Your application has been submitted successfully.";
        return RedirectToAction(nameof(ApplicationConfirmation));
    }

    public IActionResult ApplicationConfirmation()
    {
        return View();
    }
}
