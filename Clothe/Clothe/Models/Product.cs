using System.ComponentModel.DataAnnotations;

namespace Clothe.Models
{
    public class Product
    {
        public int ProductID { get; set; }
        public string ProName { get; set; }
        public string Description { get; set; }
        public string ProImg { get; set; }
        public Genre Genre { get; set; }
        public string Brand { get; set; }
        public int Qty { get; set; }
        public decimal Price { get; set; }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public ICollection<Order> Orders { get; set; }
        public ICollection<Feedback> Feedbacks { get; set; }

    }
    public enum Genre
    {
        shirt, pant, jewelry
    }
}
