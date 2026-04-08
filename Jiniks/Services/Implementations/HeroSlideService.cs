using Jiniks.Data;
using Jiniks.Models.Entities;
using Jiniks.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Services.Implementations;

public class HeroSlideService : IHeroSlideService
{
    private readonly ApplicationDbContext _context;
    private readonly ILogger<HeroSlideService> _logger;

    public HeroSlideService(ApplicationDbContext context, ILogger<HeroSlideService> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task<List<HeroSlide>> GetActiveAsync()
    {
        return await _context.HeroSlides
            .Where(h => h.IsActive)
            .OrderBy(h => h.SortOrder)
            .ToListAsync();
    }

    public async Task<List<HeroSlide>> GetAllAsync()
    {
        return await _context.HeroSlides
            .OrderBy(h => h.SortOrder)
            .ToListAsync();
    }

    public async Task<HeroSlide?> GetByIdAsync(Guid id)
    {
        return await _context.HeroSlides.FindAsync(id);
    }

    public async Task<HeroSlide> CreateAsync(HeroSlide slide)
    {
        _context.HeroSlides.Add(slide);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Created hero slide {SlideId} with headline {Headline}", slide.Id, slide.Headline);
        return slide;
    }

    public async Task<HeroSlide> UpdateAsync(HeroSlide slide)
    {
        _context.HeroSlides.Update(slide);
        await _context.SaveChangesAsync();
        _logger.LogInformation("Updated hero slide {SlideId}", slide.Id);
        return slide;
    }

    public async Task DeleteAsync(Guid id)
    {
        var slide = await _context.HeroSlides.FindAsync(id);
        if (slide != null)
        {
            _context.HeroSlides.Remove(slide);
            await _context.SaveChangesAsync();
            _logger.LogInformation("Deleted hero slide {SlideId}", id);
        }
    }
}
