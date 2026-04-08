using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers;

public class PortfolioController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ILogger<PortfolioController> _logger;

    public PortfolioController(IProjectService projectService, ILogger<PortfolioController> logger)
    {
        _projectService = projectService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
        var projects = await _projectService.GetActivePagedAsync(page, 9);
        return View(projects);
    }

    public async Task<IActionResult> Details(Guid id)
    {
        var project = await _projectService.GetByIdWithMediaAsync(id);
        if (project == null || !project.IsActive)
        {
            _logger.LogWarning("Project {ProjectId} not found or inactive", id);
            return NotFound();
        }

        return View(project);
    }
}
