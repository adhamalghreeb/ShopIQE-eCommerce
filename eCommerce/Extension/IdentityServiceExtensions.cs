using eCommerce.Core.Data;
using eCommerce.Core.entities.identity;
using eCommerce.Infrastructre.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace eCommerce.Extension
{
    public static class IdentityServiceExtensions
    {
        public static IServiceCollection AddIdentityServices(this IServiceCollection services, IConfiguration config)
        {
            // var builder = services.AddIdentityCore<AppUser>();

            services.AddDbContext<AppIdentityContext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ConnectionString"),
                b => b.MigrationsAssembly(typeof(appDBcontext).Assembly.FullName));
            });

            services.AddIdentityApiEndpoints<AppUser>(opt =>
            {
                // options
            })
            .AddRoles<IdentityRole>()
            .AddEntityFrameworkStores<AppIdentityContext>();
            

            services.AddAuthentication();
            services.AddAuthorization();

            return services;


        }
    }
}
