namespace FakeFB.Models
{
    public class UserProfile
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Location { get; set; }
        public string About { get; set; }
        public string ProfilePictureUrl { get; set; } // Path to the profile picture
        public string CoverPhotoUrl { get; set; } // Path to the cover photo
    }

}
