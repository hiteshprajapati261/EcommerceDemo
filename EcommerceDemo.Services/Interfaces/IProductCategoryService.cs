using EcommerceDemo.Models.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EcommerceDemo.Services.Interfaces
{
    public interface IProductCategoryService
    {   
        Task<IEnumerable<ComboBoxModel>> GetCategories();
    }
}
