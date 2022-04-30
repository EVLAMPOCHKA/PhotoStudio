using Microsoft.AspNetCore.Identity;

namespace EvlampochkaPhotoStudio.Models
{
    public class User
    {
        [PersonalData]
        public int Id { get; set; }

        [ProtectedPersonalData]
        public string Name { get; set; }

        [ProtectedPersonalData]
        public string Email { get; set; }

        [ProtectedPersonalData]
        public string Password { get; set; }

        [ProtectedPersonalData]
        public string PhoneNumber { get; set; }

        public Role Role { get; set; }

        public override string ToString()
        {
            return Name;
        }

        public User()
        {
        }
    }
}
