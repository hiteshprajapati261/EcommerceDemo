using EcommerceDemo.Models.Entity;
using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EcommerceDemo.Data.Interfaces
{
    public interface IProductRepository
    {
        Task<IEnumerable<ProductModel>> GetProductById(int id);

        Task<IEnumerable<ProductAttributeModel>> GetProductAttributesById(int id);

        Task<IEnumerable<ProductListModel>> GetProducts(ProductSearchModel searchModel);

        Task<ResponseModel> SaveProduct(ProductModel model);

        Task<ResponseModel> DeleteProduct(long productId);
    }
}
