using System.ComponentModel.DataAnnotations;

namespace VodusTask.Data
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
        public string? Image { get; set; }
        public decimal DiscountedPrice { get; set; }

        public DateTime OrderDate { get; set; }
    }
}
