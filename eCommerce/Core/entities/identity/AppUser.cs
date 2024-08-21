using Microsoft.AspNetCore.Identity;

namespace eCommerce.Core.entities.identity
{
    public class AppUser : IdentityUser
    {
        public string DisplayName { get; set; }
        public Address address { get; set; }
    }
}
