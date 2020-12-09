namespace EcommerceDemo.Models.Model
{
    public class ProductListModel
    {
        public long ProductId { get; set; }
        public int ProdCatId { get; set; }
        public string CategoryName { get; set; }
        public string ProdName { get; set; }
        public string ProdDescription { get; set; }
        public int TotalCount { get; set; }
    }
}
