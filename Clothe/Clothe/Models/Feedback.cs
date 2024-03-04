namespace Clothe.Models
{
    public class Feedback
    {
        public int FeedbackID { get; set; }
        public int ProductID { get; set; }
        public int UserID { get; set; }
        public string messsage { get; set; }
        public Product Product { get; set; }
        public User User { get; set; }
    }
}
