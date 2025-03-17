namespace FUST.ECommerce.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int CategoryID { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

        public Category Category { get; set; }
    }
}