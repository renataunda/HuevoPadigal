using System.ComponentModel.DataAnnotations;

namespace PadigalAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Phone { get; set; }

        [Required]
        public string Address { get; set; }

        public string? Neighborhood { get; set; }

        public string? Zone { get; set; }

        public string? DeliveryDays { get; set; }

        public string? DeliveryFrequency { get; set; }

        public int? ScheduledQuantity { get; set; }

        public string? FirstTimeProduct { get; set; }

        public string? Notes { get; set; }

        public int? Year { get; set; }

        // Navigation properties for multiple addresses and phone numbers
        public ICollection<ClientAddress> Addresses { get; set; } = new List<ClientAddress>();

        public ICollection<ClientPhone> PhoneNumbers { get; set; } = new List<ClientPhone>();
    }
}
