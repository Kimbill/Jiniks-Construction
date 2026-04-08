using Jiniks.Models.Entities;
using Jiniks.Models.Enums;
using Microsoft.EntityFrameworkCore;

namespace Jiniks.Data;

public static class SeedData
{
    public static async Task InitializeAsync(IServiceProvider serviceProvider)
    {
        using var scope = serviceProvider.CreateScope();
        var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
        var logger = scope.ServiceProvider.GetRequiredService<ILogger<ApplicationDbContext>>();

        if (await context.HeroSlides.AnyAsync())
        {
            logger.LogInformation("Database already seeded, skipping");
            return;
        }

        logger.LogInformation("Seeding database with initial data");

        var heroSlides = new List<HeroSlide>
        {
            new()
            {
                Headline = "Building Tomorrow, Today",
                Subtext = "Premium construction services for residential and commercial projects. Quality you can trust.",
                CtaText = "Get Started",
                CtaUrl = "/Booking",
                BackgroundImageUrl = "https://images.unsplash.com/photo-1504307651254-35680f356dfd?w=1920&q=80",
                SortOrder = 1,
                IsActive = true
            },
            new()
            {
                Headline = "Craftsmanship That Stands the Test of Time",
                Subtext = "From foundations to finishing, we deliver excellence at every phase of your construction project.",
                CtaText = "View Our Work",
                CtaUrl = "/Portfolio",
                BackgroundImageUrl = "https://images.unsplash.com/photo-1541888946425-d81bb19240f5?w=1920&q=80",
                SortOrder = 2,
                IsActive = true
            },
            new()
            {
                Headline = "Your Vision, Our Expertise",
                Subtext = "Let us transform your ideas into structures that inspire. Book a consultation today.",
                CtaText = "Book Now",
                CtaUrl = "/Booking",
                BackgroundImageUrl = "https://images.unsplash.com/photo-1503387762-592deb58ef4e?w=1920&q=80",
                SortOrder = 3,
                IsActive = true
            }
        };

        var services = new List<Service>
        {
            new()
            {
                Title = "Residential Construction",
                Description = "Custom home building from concept to completion. We create living spaces that reflect your lifestyle and exceed expectations.",
                IconCssClass = "fas fa-home",
                SortOrder = 1,
                IsActive = true
            },
            new()
            {
                Title = "Commercial Construction",
                Description = "Office buildings, retail spaces, and commercial facilities built to the highest standards of quality and functionality.",
                IconCssClass = "fas fa-building",
                SortOrder = 2,
                IsActive = true
            },
            new()
            {
                Title = "Renovation & Remodeling",
                Description = "Transform existing spaces with modern design and superior craftsmanship. Breathe new life into any structure.",
                IconCssClass = "fas fa-tools",
                SortOrder = 3,
                IsActive = true
            },
            new()
            {
                Title = "Project Management",
                Description = "End-to-end project oversight ensuring timely delivery, budget control, and quality assurance at every milestone.",
                IconCssClass = "fas fa-tasks",
                SortOrder = 4,
                IsActive = true
            },
            new()
            {
                Title = "Architectural Design",
                Description = "Innovative architectural solutions that blend aesthetics with structural integrity for lasting visual impact.",
                IconCssClass = "fas fa-drafting-compass",
                SortOrder = 5,
                IsActive = true
            },
            new()
            {
                Title = "Interior Finishing",
                Description = "Premium interior finishes including flooring, painting, fixtures, and custom cabinetry for a polished final product.",
                IconCssClass = "fas fa-paint-roller",
                SortOrder = 6,
                IsActive = true
            }
        };

        var projects = new List<Project>
        {
            new()
            {
                Title = "Lekki Luxury Residence",
                Description = "A 5-bedroom contemporary duplex featuring open-plan living, smart home integration, and a rooftop terrace with panoramic views of the Lagos lagoon.",
                Location = "Lekki, Lagos",
                CompletionDate = new DateTimeOffset(2025, 8, 15, 0, 0, 0, TimeSpan.Zero),
                Scope = "5-Bedroom Luxury Duplex",
                CoverImageUrl = "https://images.unsplash.com/photo-1613490493576-7fde63acd811?w=800&q=80",
                IsActive = true
            },
            new()
            {
                Title = "Abuja Business Tower",
                Description = "A 12-story commercial office tower in the heart of Abuja's central business district, featuring floor-to-ceiling glass facades and energy-efficient systems.",
                Location = "Abuja, Nigeria",
                CompletionDate = new DateTimeOffset(2024, 12, 20, 0, 0, 0, TimeSpan.Zero),
                Scope = "12-Story Commercial Tower",
                CoverImageUrl = "https://images.unsplash.com/photo-1486406146926-c627a92ad1ab?w=800&q=80",
                IsActive = true
            },
            new()
            {
                Title = "Victoria Island Apartments",
                Description = "A luxury apartment complex with 24 units, swimming pool, gym facilities, and 24-hour security. Designed for modern urban living.",
                Location = "Victoria Island, Lagos",
                CompletionDate = new DateTimeOffset(2024, 11, 10, 0, 0, 0, TimeSpan.Zero    ),
                Scope = "24-Unit Apartment Complex",
                CoverImageUrl = "https://images.unsplash.com/photo-1545324418-cc1a3fa10c00?w=800&q=80",
                IsActive = true
            },
            new()
            {
                Title = "Ikeja Shopping Plaza",
                Description = "A modern retail complex featuring 40 shop spaces, food court, underground parking, and a central atrium with natural lighting.",
                Location = "Ikeja, Lagos",
                CompletionDate = new DateTimeOffset(2025, 1, 5, 0, 0, 0, TimeSpan.Zero),
                Scope = "Retail Shopping Complex",
                CoverImageUrl = "https://images.unsplash.com/photo-1567521464027-f127ff144326?w=800&q=80",
                IsActive = true
            },
            new()
            {
                Title = "Ikoyi Waterfront Villa",
                Description = "An exclusive waterfront villa with private dock, infinity pool, and landscaped gardens spanning over 1,200 square meters.",
                Location = "Ikoyi, Lagos",
                CompletionDate = new DateTimeOffset(2024, 6, 30, 0, 0, 0, TimeSpan.Zero),
                Scope = "Waterfront Villa",
                CoverImageUrl = "https://images.unsplash.com/photo-1600596542815-ffad4c1539a9?w=800&q=80",
                IsActive = true
            },
            new()
            {
                Title = "Port Harcourt Estate",
                Description = "A gated community development featuring 15 semi-detached homes with shared recreational facilities and green spaces.",
                Location = "Port Harcourt, Rivers",
                CompletionDate = new DateTimeOffset(2025, 5, 12, 0, 0, 0, TimeSpan.Zero),
                Scope = "15-Home Estate Development",
                CoverImageUrl = "https://images.unsplash.com/photo-1600585154340-be6161a56a0c?w=800&q=80",
                IsActive = true
            }
        };

        var jobOpenings = new List<JobOpening>
        {
            new()
            {
                Title = "Senior Site Engineer",
                Location = "Lagos, Nigeria",
                Type = JobType.FullTime,
                Description = "We are looking for an experienced Site Engineer to oversee construction operations on our Lagos projects.\n\nResponsibilities:\n- Supervise daily site activities and ensure compliance with engineering standards\n- Review and interpret construction drawings and specifications\n- Coordinate with subcontractors and suppliers\n- Ensure health and safety regulations are followed\n\nRequirements:\n- BSc/BEng in Civil Engineering\n- Minimum 5 years of construction site experience\n- Strong knowledge of building codes and regulations\n- Excellent leadership and communication skills",
                IsActive = true
            },
            new()
            {
                Title = "Project Architect",
                Location = "Abuja, Nigeria",
                Type = JobType.FullTime,
                Description = "Join our design team as a Project Architect to lead architectural design for commercial and residential projects.\n\nResponsibilities:\n- Develop architectural designs and construction documents\n- Collaborate with clients to understand project vision\n- Coordinate with engineering teams for structural integration\n- Manage design reviews and approvals\n\nRequirements:\n- Master's degree in Architecture\n- Minimum 3 years of professional experience\n- Proficiency in AutoCAD, Revit, and SketchUp\n- Portfolio of completed projects",
                IsActive = true
            },
            new()
            {
                Title = "Construction Intern",
                Location = "Lagos, Nigeria",
                Type = JobType.Internship,
                Description = "A 6-month internship program for aspiring construction professionals.\n\nWhat you will learn:\n- On-site construction management\n- Material estimation and procurement\n- Quality control procedures\n- Health and safety protocols\n\nRequirements:\n- Currently enrolled in or recently graduated from a Civil Engineering or Architecture program\n- Strong willingness to learn\n- Basic knowledge of construction processes",
                IsActive = true
            }
        };

        context.HeroSlides.AddRange(heroSlides);
        context.Services.AddRange(services);
        context.Projects.AddRange(projects);
        context.JobOpenings.AddRange(jobOpenings);

        await context.SaveChangesAsync();
        logger.LogInformation("Database seeded successfully with {Slides} slides, {Services} services, {Projects} projects, {Jobs} job openings",
            heroSlides.Count, services.Count, projects.Count, jobOpenings.Count);
    }
}
