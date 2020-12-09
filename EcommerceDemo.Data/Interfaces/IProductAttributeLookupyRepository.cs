using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDemo.Data.Interfaces
{
    public interface IProductAttributeLookupRepository
    {        
        Task<IEnumerable<ProductAttributeLookupModel>> GetProductAttributeLookups(int categoryId);
    }
}
