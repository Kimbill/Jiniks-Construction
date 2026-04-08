using Jiniks.Models.Common;
using Jiniks.Models.Entities;

namespace Jiniks.Services.Interfaces;

public interface IHeroSlideService
{
    Task<List<HeroSlide>> GetActiveAsync();
    Task<List<HeroSlide>> GetAllAsync();
    Task<PaginatedList<HeroSlide>> GetAllPagedAsync(int page, int pageSize);
    Task<HeroSlide?> GetByIdAsync(Guid id);
    Task<HeroSlide> CreateAsync(HeroSlide slide);
    Task<HeroSlide> UpdateAsync(HeroSlide slide);
    Task DeleteAsync(Guid id);
}
