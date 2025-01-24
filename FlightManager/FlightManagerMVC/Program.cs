using FlightManager.Data;
using FlightManagerMVC;
using Microsoft.EntityFrameworkCore;
using FlightManager.Shared.Services.Contracts;
using FlightManager.Shared.Repos.Contracts;  // Ensure this import is added
using FlightManager.Services;  // Ensure this import is added for BookingsService
using FlightManager.Data.Repos;
using FlightManager.Shared.Extensions;  // Ensure this import is added for BookingRepository

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));

builder.Services.AutoBind(typeof(BookingsService).Assembly);
builder.Services.AutoBind(typeof(BookingRepository).Assembly);

builder.Services.AddDbContext<FlightManagerDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"]);
});

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FlightManagerDbContext>();
    // Automatically update the database
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
