using System.Diagnostics;
using Jiniks.Models;
using Jiniks.Models.ViewModels.Public;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers;

public class HomeController : Controller
{
    private readonly IHeroSlideService _heroSlideService;
    private readonly IProjectService _projectService;
    private readonly IServiceService _serviceService;
    private readonly ILogger<HomeController> _logger;

    public HomeController(
        IHeroSlideService heroSlideService,
        IProjectService projectService,
        IServiceService serviceService,
        ILogger<HomeController> logger)
    {
        _heroSlideService = heroSlideService;
        _projectService = projectService;
        _serviceService = serviceService;
        _logger = logger;
    }

    public async Task<IActionResult> Index()
    {
        _logger.LogInformation("Loading homepage");

        var model = new HomeViewModel
        {
            HeroSlides = await _heroSlideService.GetActiveAsync(),
            FeaturedProjects = (await _projectService.GetActiveAsync()).Take(6).ToList(),
            Services = await _serviceService.GetActiveAsync()
        };

        return View(model);
    }

    public IActionResult About()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
