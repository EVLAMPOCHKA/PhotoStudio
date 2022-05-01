namespace EvlampochkaPhotoStudio.Models
{
    public class Comment
    {
        public int Id { get; set; }

        public string? UserName { get; set; }

        public string? Text { get; set; }

        public int? RoomId { get; set; }

        public DateTime CreationDate { get; set; } =  DateTime.Now;

        public Room? Room { get; set; }

        public User? User { get; set; }

    }
}
