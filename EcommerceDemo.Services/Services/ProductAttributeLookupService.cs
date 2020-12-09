using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Services.Interfaces;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDemo.Services.Services
{
    public class ProductAttributeLookupService : IProductAttributeLookupService
    {
        private readonly IProductAttributeLookupRepository productAttributeLookupRepository;

        public ProductAttributeLookupService(IProductAttributeLookupRepository _productAttributeLookupRepository)
        {
            this.productAttributeLookupRepository = _productAttributeLookupRepository;
        }

        public async Task<IEnumerable<ProductAttributeLookupModel>> GetProductAttributeLookups(int categoryId)
        {
            return await productAttributeLookupRepository.GetProductAttributeLookups(categoryId);
        }
    }
}

