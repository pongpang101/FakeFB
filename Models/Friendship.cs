namespace FakeFB.Models
{
    public class Friendship
    {
        public int Id { get; set; } // Assuming there is an Id property
        public string RequestorId { get; set; }
        public string ReceiverId { get; set; }
        public bool IsAccepted { get; set; }
    }

}
