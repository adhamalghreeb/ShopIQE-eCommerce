using AutoMapper;
using Blog_Project.CORE.@interface;
using eCommerce.Core.entities;
using eCommerce.Core.Interface;
using eCommerce.Core.Specifications;
using eCommerce.DTO;
using eCommerce.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eCommerce.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductsController : ControllerBase
    {
        public IUnitOfWork unitOfWork { get; }
        private readonly IMapper _mappingProfiles;

        public ProductsController(IUnitOfWork unitOfWork, IMapper mappingProfiles)
        {
            this._mappingProfiles = mappingProfiles;
            this.unitOfWork = unitOfWork;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductReutrnDTO>>> GetProducts([FromQuery]ProductSpecParams productSpecParams)
        {
            var spec = new ProductsWithTypesandBrandSpec(productSpecParams);
            var countSpec = new ProductsWithFilterForCountSpec(productSpecParams);
            var products = await unitOfWork.productsRepository.ListAsync(spec);

            var totalItems = await unitOfWork.productsRepository.CountAsync(countSpec);

            var Result = _mappingProfiles.Map<IReadOnlyList<Product>, IReadOnlyList<ProductReutrnDTO>>(products);
            
            return Ok(new Pagination<ProductReutrnDTO>(productSpecParams.pageIndex, productSpecParams.pageSize, totalItems, Result));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var spec = new ProductsWithTypesandBrandSpec(id);
            var product = await unitOfWork.productsRepository.GetEntityWithSpec(spec);
            var Result = _mappingProfiles.Map<Product, ProductReutrnDTO>(product);
            return Ok(Result);
        }
    }
}
