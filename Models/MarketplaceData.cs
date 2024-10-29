namespace FakeFB.Models
{
    public static class MarketplaceData
    {
        public static List<MarketplaceItem> Items =
    [
        new MarketplaceItem { Id = 1, Title = "Bike", Description = "Mountain bike", Price = 200, Category = "Sports", ImageUrl = "bike.jpg", SellerName = "Alice", PostedAt = DateTime.Now },
        new MarketplaceItem { Id = 2, Title = "Laptop", Description = "Gaming laptop", Price = 1200, Category = "Electronics", ImageUrl = "laptop.jpg", SellerName = "Bob", PostedAt = DateTime.Now }
    ];
    }

}
