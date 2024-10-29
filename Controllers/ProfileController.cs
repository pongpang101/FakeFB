using FakeFB.Models; // Ensure this is the correct namespace for UserProfile
using Microsoft.AspNetCore.Mvc;

namespace FakeFB.Controllers
{
    public class ProfileController : Controller
    {
        // Display the user profile
        [Route("Profile")]
        public IActionResult Index(string id)
        {
            var userProfile = GetUserProfileById(id); // Fetch user profile data
            if (userProfile == null)
            {
                return NotFound();
            }

            return View(userProfile);
        }

        // Fetch user profile by ID
        private UserProfile GetUserProfileById(string id)
        {
            return new UserProfile
            {
                Name = "John Doe",
                Email = "john.doe@example.com",
                Location = "Thailand",
                About = "Lorem ipsum dolor sit amet.",
                ProfilePictureUrl = "/images/profile.png", // Sample profile picture URL
                CoverPhotoUrl = "/images/cover-photo.jpg", // Sample cover photo URL
                Id = 1 // Sample ID
            };
        }

        // Show the edit profile page
        public IActionResult Edit(string id)
        {
            var userProfile = GetUserProfileById(id); // Fetch user profile data
            if (userProfile == null)
            {
                return NotFound();
            }
            return View(userProfile);
        }

        // Handle edit profile form submission
        [HttpPost]
        public IActionResult Edit(UserProfile model)
        {
            if (ModelState.IsValid)
            {
                UpdateUserProfile(model); // Implement this method to update user data
                return RedirectToAction("Index", new { id = model.Id });
            }
            return View(model); // Return to the edit view if model is invalid
        }

        // Update user profile
        private void UpdateUserProfile(UserProfile model)
        {
            // Implement the logic to update the user's profile in the database
            // For example: dbContext.UserProfiles.Update(model);
        }

        // Implement logout functionality
        public IActionResult Logout()
        {
            // Log out the user
            // For example: _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
