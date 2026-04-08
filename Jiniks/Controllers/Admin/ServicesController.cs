using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Admin;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class ServicesController : Controller
{
    private readonly IServiceService _serviceService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<ServicesController> _logger;

    public ServicesController(
        IServiceService serviceService,
        ICloudinaryService cloudinaryService,
        ILogger<ServicesController> logger)
    {
        _serviceService = serviceService;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index(int page = 1)
    {
        var services = await _serviceService.GetAllPagedAsync(page, 10);
        return View("~/Views/Admin/Services/Index.cshtml", services);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/Services/Create.cshtml", new ServiceFormViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(ServiceFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Services/Create.cshtml", model);

        string? imageUrl = null;
        if (model.Image != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.Image, "services");
            imageUrl = result.Url;
        }

        var service = new Service
        {
            Title = model.Title,
            Description = model.Description,
            IconCssClass = model.IconCssClass,
            ImageUrl = imageUrl,
            SortOrder = model.SortOrder,
            IsActive = model.IsActive
        };

        await _serviceService.CreateAsync(service);
        TempData["Success"] = "Service created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();

        var model = new ServiceFormViewModel
        {
            Id = service.Id,
            Title = service.Title,
            Description = service.Description,
            IconCssClass = service.IconCssClass,
            ExistingImageUrl = service.ImageUrl,
            SortOrder = service.SortOrder,
            IsActive = service.IsActive
        };

        return View("~/Views/Admin/Services/Edit.cshtml", model);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, ServiceFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/Services/Edit.cshtml", model);

        var service = await _serviceService.GetByIdAsync(id);
        if (service == null) return NotFound();

        service.Title = model.Title;
        service.Description = model.Description;
        service.IconCssClass = model.IconCssClass;
        service.SortOrder = model.SortOrder;
        service.IsActive = model.IsActive;

        if (model.Image != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.Image, "services");
            service.ImageUrl = result.Url;
        }

        await _serviceService.UpdateAsync(service);
        TempData["Success"] = "Service updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _serviceService.DeleteAsync(id);
        TempData["Success"] = "Service deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
