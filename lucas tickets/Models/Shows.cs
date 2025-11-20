
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace lucas_tickets.Models
{


    public class Shows
    {
        [Display(Name = "Id")]
        [Key]
        public int ShowId { get; set; }

        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public string Filename { get; set; } = string.Empty;
        public DateTime Createdate { get; set; }
        public string Location { get; set; } = string.Empty;
        public string Owner { get; set; } = string.Empty;

        public int CategoryId { get; set; }
        public Category? Category { get; set; }

        public List<Payment> Payment { get; set; }

        [NotMapped]
        [Display(Name = "Photo")]
        public IFormFile? FormFile { get; set; }
    }
}