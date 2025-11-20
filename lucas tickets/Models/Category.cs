



namespace lucas_tickets.Models
{
    public class Category
    {
        public int CategoryId { get; set; }
        public string Title { get; set; } = string.Empty;

        public List<Shows>? Shows { get; set; }
    }
}
