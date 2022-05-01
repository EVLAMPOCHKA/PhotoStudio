namespace EvlampochkaPhotoStudio.Models
{
    public class Photo
    {
        public int Id { get; set; }
        public string? ImageResource { get; set; }
        public Room? Room { get; set; }
    }
}
