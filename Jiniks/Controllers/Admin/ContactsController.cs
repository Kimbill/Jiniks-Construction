using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers.Admin;

[Route("Admin/[controller]")]
public class ContactsController : Controller
{
    private readonly IContactService _contactService;

    public ContactsController(IContactService contactService)
    {
        _contactService = contactService;
    }

    [HttpGet("")]
    public async Task<IActionResult> Index()
    {
        var messages = await _contactService.GetAllAsync();
        return View("~/Views/Admin/Contacts/Index.cshtml", messages);
    }

    [HttpGet("Details/{id}")]
    public async Task<IActionResult> Details(Guid id)
    {
        var message = await _contactService.GetByIdAsync(id);
        if (message == null) return NotFound();

        if (!message.IsRead)
            await _contactService.MarkAsReadAsync(id);

        return View("~/Views/Admin/Contacts/Details.cshtml", message);
    }
}
