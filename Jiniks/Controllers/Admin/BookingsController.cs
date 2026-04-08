using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class BookingsController : Controller
{
    private readonly IBookingService _bookingService;

    public BookingsController(IBookingService bookingService)
    {
        _bookingService = bookingService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var bookings = await _bookingService.GetAllAsync();
        return View("~/Views/Admin/Bookings/Index.cshtml", bookings);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var booking = await _bookingService.GetByIdAsync(id);
        if (booking == null) return NotFound();

        if (!booking.IsRead)
            await _bookingService.MarkAsReadAsync(id);

        return View("~/Views/Admin/Bookings/Details.cshtml", booking);
    }
}
