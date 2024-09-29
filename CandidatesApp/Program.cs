using CandidatesApp.Infrastructure.Data;
using CandidatesApp.Application.Handlers; 
using MediatR;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// Configure DbContext with InMemory database (or you can use a real database if necessary)
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseInMemoryDatabase("InMemoryDatabase"));

// Register MediatR and all handlers in the Application layer
builder.Services.AddMediatR(typeof(AddExperienceHandler).Assembly);

// Register AutoMapper with profiles from the Application layer
builder.Services.AddAutoMapper(typeof(AddExperienceHandler).Assembly);

var app = builder.Build();

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
