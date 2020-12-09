using EcommerceDemo.Data.Interfaces;
using EcommerceDemo.Services.Interfaces;
using EcommerceDemo.Models.Entity;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDemo.Services.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository productRepository;

        public ProductService(IProductRepository _productRepository)
        {
            this.productRepository = _productRepository;
        }

        public async Task<IEnumerable<ProductModel>> GetProductById(int id)
        {
            return await productRepository.GetProductById(id);
        }

        public async Task<IEnumerable<ProductAttributeModel>> GetProductAttributesById(int id)
        {
            return await productRepository.GetProductAttributesById(id);
        }

        public async Task<IEnumerable<ProductListModel>> GetProducts(ProductSearchModel searchModel)
        {
            return await productRepository.GetProducts(searchModel);
        }

        public async Task<ResponseModel> SaveProduct(ProductModel model)
        {
            return await productRepository.SaveProduct(model);
        }

        public async Task<ResponseModel> DeleteProduct(long productId)
        {
            return await productRepository.DeleteProduct(productId);
        }
    }
}

