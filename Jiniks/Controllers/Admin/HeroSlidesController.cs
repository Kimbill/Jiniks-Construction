using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Admin;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class HeroSlidesController : Controller
{
    private readonly IHeroSlideService _heroSlideService;
    private readonly ICloudinaryService _cloudinaryService;
    private readonly ILogger<HeroSlidesController> _logger;

    public HeroSlidesController(
        IHeroSlideService heroSlideService,
        ICloudinaryService cloudinaryService,
        ILogger<HeroSlidesController> logger)
    {
        _heroSlideService = heroSlideService;
        _cloudinaryService = cloudinaryService;
        _logger = logger;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var slides = await _heroSlideService.GetAllAsync();
        return View("~/Views/Admin/HeroSlides/Index.cshtml", slides);
    }

    [HttpGet("Create")]
    public IActionResult Create()
    {
        return View("~/Views/Admin/HeroSlides/Create.cshtml", new HeroSlideFormViewModel());
    }

    [HttpPost("Create")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(HeroSlideFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/HeroSlides/Create.cshtml", model);

        var imageUrl = string.Empty;
        if (model.BackgroundImage != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.BackgroundImage, "hero");
            imageUrl = result.Url;
        }

        var slide = new HeroSlide
        {
            Headline = model.Headline,
            Subtext = model.Subtext,
            CtaText = model.CtaText,
            CtaUrl = model.CtaUrl,
            BackgroundImageUrl = imageUrl,
            BackgroundMediaType = model.BackgroundMediaType,
            SortOrder = model.SortOrder,
            IsActive = model.IsActive
        };

        await _heroSlideService.CreateAsync(slide);
        TempData["Success"] = "Hero slide created successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpGet("Edit/{id}")]
    public async Task<IActionResult> Edit(Guid id)
    {
        var slide = await _heroSlideService.GetByIdAsync(id);
        if (slide == null) return NotFound();

        var model = new HeroSlideFormViewModel
        {
            Id = slide.Id,
            Headline = slide.Headline,
            Subtext = slide.Subtext,
            CtaText = slide.CtaText,
            CtaUrl = slide.CtaUrl,
            ExistingBackgroundImageUrl = slide.BackgroundImageUrl,
            BackgroundMediaType = slide.BackgroundMediaType,
            SortOrder = slide.SortOrder,
            IsActive = slide.IsActive
        };

        return View("~/Views/Admin/HeroSlides/Edit.cshtml", model);
    }

    [HttpPost("Edit/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(Guid id, HeroSlideFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View("~/Views/Admin/HeroSlides/Edit.cshtml", model);

        var slide = await _heroSlideService.GetByIdAsync(id);
        if (slide == null) return NotFound();

        slide.Headline = model.Headline;
        slide.Subtext = model.Subtext;
        slide.CtaText = model.CtaText;
        slide.CtaUrl = model.CtaUrl;
        slide.BackgroundMediaType = model.BackgroundMediaType;
        slide.SortOrder = model.SortOrder;
        slide.IsActive = model.IsActive;

        if (model.BackgroundImage != null)
        {
            var result = await _cloudinaryService.UploadImageAsync(model.BackgroundImage, "hero");
            slide.BackgroundImageUrl = result.Url;
        }

        await _heroSlideService.UpdateAsync(slide);
        TempData["Success"] = "Hero slide updated successfully.";
        return RedirectToAction(nameof(Index));
    }

    [HttpPost("Delete/{id}")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _heroSlideService.DeleteAsync(id);
        TempData["Success"] = "Hero slide deleted successfully.";
        return RedirectToAction(nameof(Index));
    }
}
