using Jiniks.Models.Entities;
using Jiniks.Models.ViewModels.Public;
using Jiniks.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Jiniks.Controllers;

public class ContactController : Controller
{
    private readonly IContactService _contactService;
    private readonly ILogger<ContactController> _logger;

    public ContactController(IContactService contactService, ILogger<ContactController> logger)
    {
        _contactService = contactService;
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View(new ContactFormViewModel());
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Index(ContactFormViewModel model)
    {
        if (!ModelState.IsValid)
            return View(model);

        var message = new ContactMessage
        {
            Name = model.Name,
            Email = model.Email,
            Phone = model.Phone,
            Subject = model.Subject,
            Message = model.Message
        };

        await _contactService.CreateAsync(message);
        _logger.LogInformation("Contact message submitted by {Name}", model.Name);

        TempData["Success"] = "Your message has been sent successfully. We will respond as soon as possible.";
        return RedirectToAction(nameof(Confirmation));
    }

    public IActionResult Confirmation()
    {
        return View();
    }
}
