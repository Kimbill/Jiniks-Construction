using Jiniks.Data;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class ContactService : IContactService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<ContactService> _logger;

    public ContactService(ApplicationDbContext context, ILogger<ContactService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<ContactMessage>> GetAllAsync()
    {
        return await _context.ContactMessages
            .OrderByDescending(c => c.CreatedAt)
            .ToListAsync();
    }

    public async Task<ContactMessage?> GetByIdAsync(Guid id)
    {
        return await _context.ContactMessages.FindAsync(id);
    }

    public async Task<ContactMessage> CreateAsync(ContactMessage message)
    {
        _context.ContactMessages.Add(message);
        await _context.SaveChangesAsync();
        _logger.LogInformation("New contact message from {Name} ({Email})", message.Name, message.Email);
        return message;
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var message = await _context.ContactMessages.FindAsync(id);
        if (message != null)
        {
            message.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetUnreadCountAsync()
    {
        return await _context.ContactMessages.CountAsync(c => !c.IsRead);
    }
}
