using eCommerce.Core.entities;

namespace eCommerce.Core.Specifications
{
    public class ProductsWithFilterForCountSpec : BaseSpecifications<Product>
    {
        public ProductsWithFilterForCountSpec(ProductSpecParams productSpecParams)
            : base(x => 
            (string.IsNullOrEmpty(productSpecParams.search) || x.Name.ToLower().Contains(productSpecParams.search)) &&
            (!productSpecParams.brandId.HasValue || x.ProductBrandId == productSpecParams.brandId) &&
            (!productSpecParams.typeId.HasValue || x.ProductTypeId == productSpecParams.typeId)

            ){}

        }
}
