using Jiniks.Data;
using Jiniks.Extensions;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .Services
    .AddDatabase(builder.Configuration)
    .AddCloudinaryIntegration(builder.Configuration)
    .AddInfrastructureSettings(builder.Configuration)
    .AddApplicationServices();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

//using (var scope = app.Services.CreateScope())
//{
//    var context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

//    await context.Database.ExecuteSqlRawAsync(
//        @"ALTER DATABASE ""Jiniks_db"" REFRESH COLLATION VERSION;");
//}

await app.ApplyMigrationsAsync();
await SeedData.InitializeAsync(app.Services);

app.Run();
