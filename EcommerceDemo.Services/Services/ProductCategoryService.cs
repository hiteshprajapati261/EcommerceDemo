using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Services.Interfaces;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDemo.Services.Services
{
    public class ProductCategoryService : IProductCategoryService
    {
        private readonly IProductCategoryRepository productCategoryRepository;

        public ProductCategoryService(IProductCategoryRepository _productCategoryRepository)
        {
            this.productCategoryRepository = _productCategoryRepository;
        }
        
        public async Task<IEnumerable<ComboBoxModel>> GetCategories()
        {
            return await productCategoryRepository.GetCategories();
        }        
    }
}

