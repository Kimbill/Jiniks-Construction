using Jiniks.Models.Entities;

namespace Jiniks.Models.ViewModels.Public;

public class HomeViewModel
{
    public List<HeroSlide> HeroSlides { get; set; } = [];
    public List<Project> FeaturedProjects { get; set; } = [];
    public List<Service> Services { get; set; } = [];
}
