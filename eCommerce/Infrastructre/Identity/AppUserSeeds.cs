using eCommerce.Core.entities;
using eCommerce.Core.entities.identity;
using Microsoft.AspNetCore.Identity;

namespace eCommerce.Infrastructre.Identity
{
    public class AppUserSeeds
    {
        public static async Task SeedUser(UserManager<AppUser> manager)
        {
            if (!manager.Users.Any())
            {
                var user = new AppUser
                {
                    DisplayName = "Bob",
                    Email = "bob@test.com",
                    UserName = "bob@test.com",
                    address = new Address
                    {
                        FirstName = "Bob",
                        Lastname = "Bobbity",
                        Street = "10 The Street",
                        City = "New York",
                        State = "NY",
                        Zipcode = "90210"
                    }
                };

                await manager.CreateAsync(user, "Pa$$w0rd");
            }

            
        }
    }
}
