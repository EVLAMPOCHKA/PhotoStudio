using Microsoft.AspNetCore.Identity;

namespace EvlampochkaPhotoStudio.Models
{
    public class User : IdentityUser
    {
        public List<Favorite>? Favorites { get; set; }
    }
}
