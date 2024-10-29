using FakeFB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FakeFB.Controllers
{
   
    public class FriendsController : Controller
    {
        private static readonly List<Friendship> _friendships = new List<Friendship>();
        private static readonly List<User> _users = new List<User>();

        [Route("Friends")]
        public IActionResult Index()
        {
            var friendsUrl = Url.Action("Index", "Friends");
            var currentUserId = GetCurrentUserId();
            var friendships = _friendships
                .Where(f => f.RequestorId == currentUserId || f.ReceiverId == currentUserId)
                .ToList();

            var friendViewModels = friendships.Select(f =>
            {
                var friendId = f.RequestorId == currentUserId ? f.ReceiverId : f.RequestorId;
                var friend = _users.FirstOrDefault(u => u.Id == friendId);
                return friend != null ? new FriendViewModel
                {
                    Id = _friendships.Count + 1,
                    FirstName = friend.FirstName,
                    LastName = friend.LastName,
                    Email = friend.Email,
                    Status = f.IsAccepted ? "Accepted" : "Pending"
                } : null;
            }).Where(vm => vm != null).ToList();

            return View(friendViewModels);
        }

        // POST: /Friends/SendFriendRequest
        [HttpPost]
        public IActionResult SendFriendRequest(string receiverId)
        {
            var currentUserId = GetCurrentUserId();
            if (currentUserId == receiverId)
            {
                TempData["Message"] = "You cannot send a friend request to yourself.";
                return RedirectToAction("Index");
            }

            var existingFriendship = _friendships.Any(f =>
                (f.RequestorId == currentUserId && f.ReceiverId == receiverId) ||
                (f.RequestorId == receiverId && f.ReceiverId == currentUserId));

            if (existingFriendship)
            {
                TempData["Message"] = "Friend request already exists.";
                return RedirectToAction("Index");
            }

            var friendship = new Friendship
            {
                Id = _friendships.Count + 1,
                RequestorId = currentUserId,
                ReceiverId = receiverId,
                IsAccepted = false
            };

            _friendships.Add(friendship);

            TempData["Message"] = "Friend request sent.";
            return RedirectToAction("Index");
        }

        // Helper method to get the current user's ID
        private string GetCurrentUserId()
        {
            return User.FindFirst("sub")?.Value;
        }

        [HttpPost]
        public IActionResult SearchFriends(string searchQuery)
        {
            var currentUserId = GetCurrentUserId();
            var friends = _friendships
                .Where(f => f.RequestorId == currentUserId || f.ReceiverId == currentUserId)
                .Select(f => f.RequestorId == currentUserId ? f.ReceiverId : f.RequestorId)
                .Distinct()
                .Select(id => _users.FirstOrDefault(u => u.Id == id))
                .Where(u => u != null &&
                    (u.FirstName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                     u.LastName.Contains(searchQuery, StringComparison.OrdinalIgnoreCase) ||
                     u.Email.Contains(searchQuery, StringComparison.OrdinalIgnoreCase)))
                .ToList();

            return View("Index", friends);
        }

        [HttpPost]
        public IActionResult RemoveFriend(string id)
        {
            var friendship = _friendships.FirstOrDefault(f =>
                (f.RequestorId == GetCurrentUserId() && f.ReceiverId == id) ||
                (f.RequestorId == id && f.ReceiverId == GetCurrentUserId()));

            if (friendship != null)
            {
                _friendships.Remove(friendship);
                TempData["Message"] = "Friend removed.";
            }

            return RedirectToAction("Index");
        }


        public IActionResult FriendRequests()
        {
            // Sample data
            var friendRequests = new List<FriendViewModel>
        {
            new FriendViewModel { Id = 1, FirstName = "John", LastName = "Doe", Email = "john.doe@example.com", Status = "Online" },
            new FriendViewModel { Id = 2, FirstName = "Jane", LastName = "Smith", Email = "jane.smith@example.com", Status = "Offline" },
            new FriendViewModel { Id = 3, FirstName = "Emily", LastName = "Jones", Email = "emily.jones@example.com", Status = "Offline"},
        };

            return View(friendRequests);
        }
        // Accept Friend Request
        [HttpPost]
        public IActionResult AcceptFriendRequest(int id)
        {
            // Logic to accept friend request using the id
            return RedirectToAction("FriendRequests");
        }

        // Decline Friend Request
        [HttpPost]
        public IActionResult DeclineFriendRequest(int id)
        {
            // Logic to decline friend request using the id
            return RedirectToAction("FriendRequests");
        }
    }
}
