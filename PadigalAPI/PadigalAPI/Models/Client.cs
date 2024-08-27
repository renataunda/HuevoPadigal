using System.ComponentModel.DataAnnotations;

namespace PadigalAPI.Models
{
    public class Client
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [Required]
        public DateTime RegistrationDate { get; set; }

        [Required]
        [StringLength(100)]
        [EmailAddress]
        public string Email { get; set; }

        public bool IsActive { get; set; }

        public string Notes { get; set; }

        [Required]
        public ClientType ClientType { get; set; }

        // Navigation properties for multiple addresses and phone numbers
        public ICollection<ClientAddress> Addresses { get; set; } = new List<ClientAddress>();

        public ICollection<ClientPhone> PhoneNumbers { get; set; } = new List<ClientPhone>();
    }

    public enum ClientType
    {
        Mayorista,
        Minorista,
        Otro
    }
}
