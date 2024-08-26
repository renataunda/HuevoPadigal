using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadigalAPI.Models
{
    public class ClientPhone
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Phone { get; set; }

        [ForeignKey("Client")]
        public int ClientId { get; set; }

        public Client Client { get; set; }
    }
}
