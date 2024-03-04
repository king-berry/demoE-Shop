namespace Clothe.Models
{
    public class Order
    {
        public int OrderID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string Status { get; set; }
        public Payment Payment { get; set; }
        public decimal total { get; set; } = 0;
        public ICollection<Product> Product { get; set; }
        public User User { get; set; }
    }
    public enum Payment
    {
        cash, online
    }
}
