using Jiniks.Models.Entities;
using Jiniks.Models.Enums;
using Jiniks.Models.ViewModels.Admin;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class ProjectsController : Controller
{
    private readonly IProjectService _projectService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<ProjectsController> _logger;

    public ProjectsController(
        IProjectService projectService,
        ICloudinaryService cloudinaryService,
        ILogger<ProjectsController> logger)
    {
        _projectService = projectService;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var projects = await _projectService.GetAllAsync();
        return View("~/Views/Admin/Projects/Index.cshtml", projects);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Projects/Create.cshtml", new ProjectFormViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ProjectFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Projects/Create.cshtml", model);

        var coverImageUrl = string.Empty;
        if (model.CoverImage != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.CoverImage, "projects");
            coverImageUrl = result.Url;
        }

        var project = new Project
        {
            Title = model.Title,
            Description = model.Description,
            Location = model.Location,
            CompletionDate = model.CompletionDate,
            Scope = model.Scope,
            CoverImageUrl = coverImageUrl,
            IsActive = model.IsActive
        };

        await _projectService.CreateAsync(project);
        TempData["Success"] = "Project created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var project = await _projectService.GetByIdWithMediaAsync(id);
        if (project == null) return NotFound();

        var model = new ProjectFormViewModel
        {
            Id = project.Id,
            Title = project.Title,
            Description = project.Description,
            Location = project.Location,
            CompletionDate = project.CompletionDate,
            Scope = project.Scope,
            ExistingCoverImageUrl = project.CoverImageUrl,
            IsActive = project.IsActive
        };

        ViewBag.Media = project.Media.OrderBy(m => m.SortOrder).ToList();
        return View("~/Views/Admin/Projects/Edit.cshtml", model);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ProjectFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Projects/Edit.cshtml", model);

        var project = await _projectService.GetByIdAsync(id);
        if (project == null) return NotFound();

        project.Title = model.Title;
        project.Description = model.Description;
        project.Location = model.Location;
        project.CompletionDate = model.CompletionDate;
        project.Scope = model.Scope;
        project.IsActive = model.IsActive;

        if (model.CoverImage != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.CoverImage, "projects");
            project.CoverImageUrl = result.Url;
        }

        await _projectService.UpdateAsync(project);
        TempData["Success"] = "Project updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _projectService.DeleteAsync(id);
        TempData["Success"] = "Project deleted successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("AddMedia/{projectId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> AddMedia(Guid projectId, IFormFile file, MediaType mediaType)
    {
        var project = await _projectService.GetByIdAsync(projectId);
        if (project == null) return NotFound();

        CloudinaryUploadResult uploadResult;
        if (mediaType == MediaType.Video)
            uploadResult = await _cloudinaryService.UploadVideoAsync(file, "projects/media");
        else
            uploadResult = await _cloudinaryService.UploadImageAsync(file, "projects/media");

        var media = new ProjectMedia
        {
            ProjectId = projectId,
            MediaUrl = uploadResult.Url,
            PublicId = uploadResult.PublicId,
            MediaType = mediaType,
            SortOrder = 0
        };

        await _projectService.AddMediaAsync(media);
        TempData["Success"] = "Media added successfully.";
        return RedirectToAction(nameof(Edit), new { id = projectId });
    }

    [HttpPost("DeleteMedia/{mediaId}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteMedia(Guid mediaId, Guid projectId)
    {
        await _projectService.DeleteMediaAsync(mediaId);
        TempData["Success"] = "Media removed successfully.";
        return RedirectToAction(nameof(Edit), new { id = projectId });
    }
}
