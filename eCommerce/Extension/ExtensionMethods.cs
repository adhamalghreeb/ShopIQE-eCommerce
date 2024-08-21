using Blog_Project.CORE.@interface;
using Blog_Project.EF.RepositoryPattern;
using eCommerce.Core.Data;
using eCommerce.Core.entities;
using eCommerce.Core.entities.identity;
using eCommerce.Core.Interface;
using eCommerce.DTO;
using eCommerce.Erros;
using eCommerce.Helpers;
using eCommerce.Infrastructre;
using eCommerce.Infrastructre.Data;
using eCommerce.Infrastructre.Identity;
using eCommerce.Infrastructre.RepositoryPattern;
using Microsoft.AspNetCore.Connections;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using StackExchange.Redis;
using System.Security.Authentication;
using System.Security.Claims;

namespace eCommerce.Extension
{
    public static class AppExtensionMethods
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services, IConfiguration config)
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddSingleton<IConnectionMultiplexer>(c =>
            {
                var options = ConfigurationOptions.Parse(config.GetConnectionString("Redis"));
                return ConnectionMultiplexer.Connect(options);
            });
            services.AddSingleton<IResponseCache, ResponseCache>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<IPaymentService, PaymentService>();
            services.AddScoped<IOrderRepository, OrderRepository>();

            services.AddAutoMapper(typeof(MappingProfiles).Assembly);
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

            services.AddDbContext<appDBcontext>(options =>
            {
                options.UseSqlServer(config.GetConnectionString("ConnectionString"),
                b => b.MigrationsAssembly(typeof(appDBcontext).Assembly.FullName));
            });

            

            



            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                    .Where(e => e.Value.Errors.Count > 0)
                    .SelectMany(x => x.Value.Errors)
                    .Select(x => x.ErrorMessage).ToArray();

                    var errorResponse = new ApiValidationError
                    {
                        Errors = errors
                    };

                    return new BadRequestObjectResult(errorResponse);
                };
            }
            );

            return services;
        }

        public static async Task<AppUser> GetUserByEmail(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users.FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userToReturn == null)
            {
                throw new AuthenticationException("Email is not found");
            }

            return userToReturn;
        }

        public static async Task<AppUser> GetUserByEmailWithAddress(this UserManager<AppUser> userManager, ClaimsPrincipal user)
        {
            var userToReturn = await userManager.Users
                .Include(x => x.address)
                .FirstOrDefaultAsync(x => x.Email == user.GetEmail());

            if (userToReturn == null)
            {
                throw new AuthenticationException("Email is not found");
            }

            return userToReturn;
        }

        public static string GetEmail(this ClaimsPrincipal user)
        {
            var email = user.FindFirstValue(ClaimTypes.Email) ?? throw new AuthenticationException("Email is not found");

            return email;
        }

        public static AddressDto? toDTO(this Address address)
        {
            if (address == null) return null;

            return new AddressDto
            {
                FirstName = address.FirstName,
                LastName = address.Lastname,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zipcode = address.Zipcode
            };
        }

        public static Address toEntitty(this AddressDto address)
        {
            if (address == null) throw new ArgumentException(nameof(address));

            return new Address
            {
                FirstName = address.FirstName,
                Lastname = address.LastName,
                Street = address.Street,
                City = address.City,
                State = address.State,
                Zipcode = address.Zipcode
            };
        }

        public static void UpdateFromDTO(this Address address ,AddressDto addressDTO)
        {
            // if (address == null) throw new ArgumentException(nameof(addressDTO));
            if (addressDTO == null) throw new ArgumentException(nameof(addressDTO));


            address.FirstName = addressDTO.FirstName;
            address.Lastname = addressDTO.LastName;
            address.Street = addressDTO.Street;
            address.City = addressDTO.City;
            address.State = addressDTO.State;
            address.Zipcode = addressDTO.Zipcode;
           
        }
    }
}
