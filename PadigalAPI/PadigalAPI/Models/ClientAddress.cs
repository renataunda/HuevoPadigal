using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadigalAPI.Models
{
    public class ClientAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Neighborhood { get; set; }

        public string? Zone { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
