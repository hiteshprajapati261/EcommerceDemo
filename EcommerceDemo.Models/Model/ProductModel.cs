using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models.Model
{
    public class ProductModel
    {
        public ProductModel() {
            ProductAttributes = new List<ProductAttributeModel>();
        }

        public long ProductId { get; set; }

        [Required(ErrorMessage = "Please select product category")]
        public int ProdCatId { get; set; }

        [Required(ErrorMessage = "Please enter product name")]
        public string ProdName { get; set; }

        [Required(ErrorMessage = "Please enter product description")]
        public string ProdDescription { get; set; }

        public IEnumerable<ProductAttributeModel> ProductAttributes { get; set; }
    }
}
