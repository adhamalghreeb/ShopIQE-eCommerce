using eCommerce.Core.Data;
using eCommerce.Core.entities;
using eCommerce.Core.entities.identity;
using Microsoft.AspNetCore.Identity;
using System.Reflection;
using System.Text.Json;

namespace eCommerce.Infrastructre.Data
{
    public class SeedappDBcontext
    {
        public static async Task SeedAsync(appDBcontext context, ILoggerFactory loggerFactory, UserManager<AppUser> userManager)
        {
			try
			{
				var path = "C:\\Users\\Adham\\source\\repos\\eCommerce\\eCommerce\\Infrastructre";
                if(!userManager.Users.Any(x => x.UserName == "admin@test.com")) // if there is no admin , create it
                {
                    var user = new AppUser
                    {
                        UserName = "admin@test.com",
                        Email = "admin@test.com",
                        DisplayName = "admin"
                    };

                    await userManager.CreateAsync(user , "Pa$$w0rd");
                    await userManager.AddToRoleAsync(user, "Admin");

                }
				if (!context.ProductBrands.Any())
				{
                    var brandsData =
                       File.ReadAllText(path + @"\\Data\\SeedData\\brands.json");
                    var brands = JsonSerializer.Deserialize<List<ProductBrand>>(brandsData);

                    foreach (var item in brands)
                    {
                        context.ProductBrands.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                
                if (!context.DeliveryMethods.Any())
                {
                    var dmData =
                        File.ReadAllText(path + @"\\Data\\SeedData\\delivery.json");

                    var methods = JsonSerializer.Deserialize<List<DeliveryMethod>>(dmData);

                    foreach (var item in methods)
                    {
                        context.DeliveryMethods.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
                if (!context.ProductTypes.Any())
                {
                    var typesData =
                        File.ReadAllText(path + @"/Data/SeedData/types.json");

                    var types = JsonSerializer.Deserialize<List<ProductType>>(typesData);

                    foreach (var item in types)
                    {
                        context.ProductTypes.Add(item);
                    }

                    await context.SaveChangesAsync();
                }

                if (!context.Products.Any())
                {
                    var productsData =
                        File.ReadAllText(path + @"/Data/SeedData/products.json");

                    var products = JsonSerializer.Deserialize<List<Product>>(productsData);

                    foreach (var item in products)
                    {
                        context.Products.Add(item);
                    }

                    await context.SaveChangesAsync();
                }
            }
			catch (Exception ex)
			{
                var logger = loggerFactory.CreateLogger<SeedappDBcontext>();
                logger.LogError(ex.Message);
            }
        }
    }
}
