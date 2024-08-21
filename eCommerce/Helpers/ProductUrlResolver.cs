using AutoMapper;
using eCommerce.Core.entities;
using eCommerce.DTO;
using Microsoft.IdentityModel.Tokens;

namespace eCommerce.Helpers
{
    public class ProductUrlResolver : IValueResolver<Product, ProductReutrnDTO, string>
    {
        private readonly IConfiguration _configuration;

        public ProductUrlResolver(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        public string Resolve(Product source, ProductReutrnDTO destination, string destMember, ResolutionContext context)
        {
            if(!string.IsNullOrEmpty(source.PictureUrl))
            {
                return _configuration["ApiUrl"] + source.PictureUrl;
            }

            return null;
        }
    }
}
