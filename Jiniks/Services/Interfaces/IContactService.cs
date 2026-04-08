using Jiniks.Models.Common;
using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IContactService
{
    Task<List<ContactMessage>> GetAllAsync();
    Task<PaginatedList<ContactMessage>> GetAllPagedAsync(int page, int pageSize);
    Task<ContactMessage?> GetByIdAsync(Guid id);
    Task<ContactMessage> CreateAsync(ContactMessage message);
    Task MarkAsReadAsync(Guid id);
    Task<int> GetUnreadCountAsync();
}
