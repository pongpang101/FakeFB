using FakeFB.Models; // Ensure you have your models defined here
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using System.Security.Claims;

namespace FakeFB.Controllers
{
    public class AccountController : Controller
    {
        private static readonly List<User> _users = []; // In-memory user storage

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check for the default user
                var defaultUser = _users.FirstOrDefault(u => u.Email == "default@example.com" && u.Password == "Password123");

                // If the user entered the default account credentials
                if (model.Email == "default@example.com" && model.Password == "Password123")
                {
                    await SignInUser(defaultUser?.Email ?? "Guest");
                    return RedirectToAction("Index", "Home");
                }

                // Check other registered users
                var user = _users.FirstOrDefault(u => u.Email == model.Email && u.Password == model.Password);
                if (user != null)
                {
                    await SignInUser(user.Email);
                    return RedirectToAction("Index", "Home");
                }

                ModelState.AddModelError(string.Empty, "Invalid login attempt.");
            }
            return View(model);
        }

        private async Task SignInUser(string email)
        {
            var claims = new List<Claim>
            {
                new(ClaimTypes.Name, email)
            };

            var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

            // Sign in the user
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, claimsPrincipal);
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                // Check if the user already exists
                if (_users.Any(u => u.Email == model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Email already in use.");
                    return View(model);
                }

                // Create a new user
                var user = new User
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    Password = model.Password
                };

                // Add the user to in-memory storage
                _users.Add(user);

                return RedirectToAction("Login", "Account");
            }
            return View(model);
        }

        public async Task<IActionResult> Logout()
        {
            // Clear the authentication cookie
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Login", "Account");
        }
    }
}
