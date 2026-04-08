using Jiniks.Models.ViewModels.Admin;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin")]
[Route("Admin/[controller]")]
public class DashboardController : Controller
{
    private readonly IProjectService _projectService;
    private readonly IServiceService _serviceService;
    private readonly IJobService _jobService;
    private readonly IBookingService _bookingService;
    private readonly IContactService _contactService;

    public DashboardController(
        IProjectService projectService,
        IServiceService serviceService,
        IJobService jobService,
        IBookingService bookingService,
        IContactService contactService)
    {
        _projectService = projectService;
        _serviceService = serviceService;
        _jobService = jobService;
        _bookingService = bookingService;
        _contactService = contactService;
    }

    [HttpGet("")]
    [HttpGet("Index")]
    public async Task<IActionResult> Index()
    {
        var model = new AdminDashboardViewModel
        {
            TotalProjects = (await _projectService.GetAllAsync()).Count,
            TotalServices = (await _serviceService.GetAllAsync()).Count,
            TotalJobs = (await _jobService.GetAllAsync()).Count,
            UnreadBookings = await _bookingService.GetUnreadCountAsync(),
            UnreadContacts = await _contactService.GetUnreadCountAsync(),
            UnreadApplications = await _jobService.GetUnreadApplicationCountAsync()
        };

        return View("~/Views/Admin/Dashboard/Index.cshtml", model);
    }
}
