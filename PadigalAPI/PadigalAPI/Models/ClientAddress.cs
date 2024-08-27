using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadigalAPI.Models
{
    public class ClientAddress
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(250)]
        public string AddressLine { get; set; }

        [Required]
        [StringLength(100)]
        public string Neighborhood { get; set; }

        [Required]
        [StringLength(100)]
        public string Zone { get; set; }

        public bool IsActive { get; set; }

        // Foreign key
        [ForeignKey("Client")]
        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
