namespace EvlampochkaPhotoStudio.Models
{
    public class BookedDates
    {      
        public int Id { get; set; }
        public Room Room { get; set; }
        public int? RoomId { get; set; }
        public DateTime Date { get; set; }
    }
}
