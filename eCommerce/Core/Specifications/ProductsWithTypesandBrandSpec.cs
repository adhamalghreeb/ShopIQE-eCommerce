using eCommerce.Core.entities;

namespace eCommerce.Core.Specifications
{
    public class ProductsWithTypesandBrandSpec : BaseSpecifications<Product>
    {
        public ProductsWithTypesandBrandSpec(ProductSpecParams productSpecParams)
            :base(x =>
            (string.IsNullOrEmpty(productSpecParams.search) || x.Name.ToLower().Contains(productSpecParams.search)) &&
            (!productSpecParams.brandId.HasValue || x.ProductBrandId == productSpecParams.brandId) &&
            (!productSpecParams.typeId.HasValue || x.ProductTypeId == productSpecParams.typeId)
                
            )
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
            AddOrderBy(x => x.Name);

            if (!string.IsNullOrEmpty(productSpecParams.sort))
            {
                switch (productSpecParams.sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.Price);
                        break;
                    case "priceDesc":
                        AddOrderByDesc(p => p.Price);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }

    }

        public ProductsWithTypesandBrandSpec(int id) : base(x => x.Id == id)
        {
            AddInclude(x => x.ProductType);
            AddInclude(x => x.ProductBrand);
        }

    }
}
