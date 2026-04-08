using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Public;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers;

public class BookingController : Controller
{
    private readonly IBookingService _bookingService;
    private readonly ILogger<BookingController> _logger;

    public BookingController(IBookingService bookingService, ILogger<BookingController> logger)
    {
        _bookingService = bookingService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new BookingFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(BookingFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var booking = new Booking
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            ProjectType = model.ProjectType,
            BudgetRange = model.BudgetRange,
            Message = model.Message
        };

        await _bookingService.CreateAsync(booking);
        _logger.LogInformation("Booking submitted successfully by {Name}", model.Name);

        TempData["Success"] = "Your booking request has been submitted successfully. We will get back to you shortly.";
        return RedirectToAction(nameof(Confirmation));
    }

    public IActionResult Confirmation()
    {
        return View();
    }
}
