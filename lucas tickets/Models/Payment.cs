namespace lucas_tickets.Models
{
    public class Payment
    {
        public int Paymentid { get; set; }
        public string body { get; set; } = string.Empty;
        public string Author { get; set; } = string.Empty ;
        public DateTime CreateDate { get; set; }

        
        public int ShowID { get; set; }

        
        public Shows? Shows { get; set; } 
    }
}
