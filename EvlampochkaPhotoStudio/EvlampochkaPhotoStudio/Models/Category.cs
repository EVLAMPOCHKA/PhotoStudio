namespace EvlampochkaPhotoStudio.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public override string? ToString() => this?.Name;
    }

   
}
