using FakeFB.Models; // Ensure you have your models defined here
using Microsoft.AspNetCore.Mvc;

namespace FakeFB.Controllers
{
    public class MarketplaceController : Controller
    {
        // Display all items
        public IActionResult Index()
        {
            return View(MarketplaceData.Items);
        }

        // Display item details
        public IActionResult Details(int id)
        {
            var item = MarketplaceData.Items.FirstOrDefault(i => i.Id == id);
            if (item == null) return NotFound();
            return View(item);
        }

        // Create a new item form
        public IActionResult Create() => View();

        // Post action to add a new item
        [HttpPost]
        public IActionResult Create(MarketplaceItem item)
        {
            item.Id = MarketplaceData.Items.Max(i => i.Id) + 1;
            item.PostedAt = DateTime.Now;
            MarketplaceData.Items.Add(item);
            return RedirectToAction(nameof(Index));
        }

        // Delete an item
        public IActionResult Delete(int id)
        {
            var item = MarketplaceData.Items.FirstOrDefault(i => i.Id == id);
            if (item != null) MarketplaceData.Items.Remove(item);
            return RedirectToAction(nameof(Index));
        }
    }

}
