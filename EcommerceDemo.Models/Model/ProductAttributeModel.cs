using System.ComponentModel.DataAnnotations;

namespace EcommerceDemo.Models.Model
{
    public class ProductAttributeModel
    {
        public long ProductId { get; set; }
        public int AttributeId { get; set; }

        [Required(ErrorMessage = "Please enter attribute value")]
        public string AttributeValue { get; set; }
    }
}
