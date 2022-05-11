namespace EvlampochkaPhotoStudio.Models
{
    public class Favorite
    {
        public int Id { get; set; }

        public Room Room { get; set; }

        public int RoomId { get; set; }

        public User User { get; set; }
    }
}
