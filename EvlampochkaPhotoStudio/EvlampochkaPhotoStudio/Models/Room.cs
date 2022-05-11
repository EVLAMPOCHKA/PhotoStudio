using System.ComponentModel.DataAnnotations.Schema;

namespace EvlampochkaPhotoStudio.Models
{
    public class Room
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public Category Category { get; set; }
        public int CategoryId { get; set; }
        public string Description { get; set; }
        public double Price { get; set; }
        public string? MainImage { get; set; }
        public List<Comment>? Comments { get; set; }
        [NotMapped]
        public DateTime BookedDate { get; set; }
        [NotMapped]
        public DateTime CreationDate { get; set; }

    }
}
