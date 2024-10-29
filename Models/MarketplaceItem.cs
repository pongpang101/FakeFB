using System.ComponentModel.DataAnnotations;

namespace FakeFB.Models
{
    public class MarketplaceItem
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string Category { get; set; }
        public string ImageUrl { get; set; }
        public string SellerName { get; set; }
        public DateTime PostedAt { get; set; }
    }


}
