namespace EcommerceDemo.Core.Entity
{
    public class ProductEntity : BaseEntity
    {
        public long ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
    }
}
