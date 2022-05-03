namespace EvlampochkaPhotoStudio.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public User User { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }
        public DateTime CreationDate { get; set; } = DateTime.Now;
        public DateTime BookingDate { get; set; }
    }
}
