namespace VodusTaskB.Models
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public decimal DiscountedPrice { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
