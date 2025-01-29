using FlightManager.Data;
using FlightManager.Data.Repos;
using FlightManager.Services;
using FlightManagerMVC;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using FlightManager.Shared.Extensions;
using EntityFrameworkCore.UseRowNumberForPaging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();


builder.Services.AddDbContext<FlightManagerDbContext>(options =>
{

    options.UseSqlServer(builder.Configuration["ConnectionStrings:DefaultConnection"],
        r => r.UseRowNumberForPaging());
});

builder.Services.AutoBind(typeof(BookingsService).Assembly);
builder.Services.AutoBind(typeof(BookingRepository).Assembly);

builder.Services.AddAutoMapper(m => m.AddProfile(new AutoMapperConfiguration()));

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<FlightManagerDbContext>();
    // Automatically update database
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
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
