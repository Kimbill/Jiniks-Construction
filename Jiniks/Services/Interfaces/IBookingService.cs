using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IBookingService
{
    Task<List<Booking>> GetAllAsync();
    Task<Booking?> GetByIdAsync(Guid id);
    Task<Booking> CreateAsync(Booking booking);
    Task MarkAsReadAsync(Guid id);
    Task<int> GetUnreadCountAsync();
}
