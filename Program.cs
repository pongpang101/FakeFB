using FakeFB.Models; // Ensure this includes your User model
using Microsoft.AspNetCore.Authentication.Cookies;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSession(); // Add session support
builder.Logging.ClearProviders();
builder.Logging.AddConsole();

// Register the users list as a service
builder.Services.AddSingleton<List<User>>(); // In-memory user list

// Configure Authentication
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(options =>
    {
        options.LoginPath = "/Account/Login"; // Adjust to your login path
        options.LogoutPath = "/Account/Logout"; // Adjust to your logout path
    });

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/Account/Login"; // Adjust according to your login path
});

var app = builder.Build();

// Create a default user
CreateDefaultUser(app.Services);

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseSession(); // Use session
app.UseAuthentication(); // Use authentication
app.UseAuthorization(); // Use authorization

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.MapControllerRoute(
    name: "marketplace",
    pattern: "Marketplace/{action=Index}/{id?}",
    defaults: new { controller = "Marketplace", action = "Index" });


app.Run();

static void CreateDefaultUser(IServiceProvider serviceProvider)
{
    var users = serviceProvider.GetService<List<User>>();

    if (users != null && users.Count == 0) // Check if any users exist
    {
        // Create a default user
        users.Add(new User
        {
            FirstName = "Default",
            LastName = "User",
            Email = "default@example.com",
            Password = "Password123" // Ensure you handle passwords securely in a real app!
        });
    }
}
