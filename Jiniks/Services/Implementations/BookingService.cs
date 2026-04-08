using Jiniks.Data;
using Jiniks.Models.Common;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class BookingService : IBookingService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<BookingService> _logger;

    public BookingService(ApplicationDbContext context, ILogger<BookingService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<Booking>> GetAllAsync()
    {
        return await _context.Bookings
            .OrderByDescending(b => b.CreatedAt)
            .ToListAsync();
    }

    public async Task<PaginatedList<Booking>> GetAllPagedAsync(int page, int pageSize)
    {
        var query = _context.Bookings
            .OrderByDescending(b => b.CreatedAt);

        return await PaginatedList<Booking>.CreateAsync(query, page, pageSize);
    }

    public async Task<Booking?> GetByIdAsync(Guid id)
    {
        return await _context.Bookings.FindAsync(id);
    }

    public async Task<Booking> CreateAsync(Booking booking)
    {
        _context.Bookings.Add(booking);
        await _context.SaveChangesAsync();
        _logger.LogInformation("New booking received from {Name} ({Email})", booking.Name, booking.Email);
        return booking;
    }

    public async Task MarkAsReadAsync(Guid id)
    {
        var booking = await _context.Bookings.FindAsync(id);
        if (booking != null)
        {
            booking.IsRead = true;
            await _context.SaveChangesAsync();
        }
    }

    public async Task<int> GetUnreadCountAsync()
    {
        return await _context.Bookings.CountAsync(b => !b.IsRead);
    }
}
