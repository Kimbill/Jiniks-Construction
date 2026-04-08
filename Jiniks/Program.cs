using Jiniks.Data;
using Jiniks.Extensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services
    .AddControllersWithViews()
    .Services
    .AddDatabase(builder.Configuration)
    .AddCloudinaryIntegration(builder.Configuration)
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

await app.ApplyMigrationsAsync();
await SeedData.InitializeAsync(app.Services);

app.Run();
