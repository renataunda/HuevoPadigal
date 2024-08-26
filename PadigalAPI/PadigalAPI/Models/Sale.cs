using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadigalAPI.Models
{
    public class Sale
    {

        [Key]
        public int Id { get; set; }      

        [Required]
        public DateTime Date { get; set; }

        [Required]
        public string Concept { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Required]
        public int Quantity { get; set; }
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }

        // New columns
        public bool Recurring { get; set; }
        public string? Frequency { get; set; }
    }
}
