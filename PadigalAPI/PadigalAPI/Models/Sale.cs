using Newtonsoft.Json;
using PadigalAPI.Converters;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadigalAPI.Models
{
    public class Sale
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public DateTime OrderDate { get; set; }

        [Required]
        public DateTime DeliveryDate { get; set; }

        [Required]
        [JsonConverter(typeof(ProductTypeConverter))]
        public ProductType ProductType { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal UnitPrice { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }

        [Required]
        public bool Recurring { get; set; }

        [JsonConverter(typeof(FrequencyConverter))]
        public Frequency? Frequency { get; set; }

        // List to store weights of each box
        public List<decimal>? BoxWeights { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalWeight { get; set; }

        [JsonConverter(typeof(PaymentTypeConverter))]
        public PaymentType? PaymentType { get; set; }

        [Required]
        public bool IsPaid { get; set; }
      
        public string Notes { get; set; }

        // Relationship with Client
        [Required]
        public int ClientId { get; set; }

        [ForeignKey("ClientId")]
        public Client Client { get; set; }
        
        // Store the specific phone number and address IDs for this sale
        [Required]
        public int PhoneNumberId { get; set; }

        [ForeignKey("PhoneNumberId")]
        public ClientPhone PhoneNumber { get; set; }

        [Required]
        public int AddressId { get; set; }

        [ForeignKey("AddressId")]
        public ClientAddress Address { get; set; }
    }
  
    public enum ProductType
    {
        Docena,
        Cartera,
        Caja,
        Otro
    }

    public enum Frequency
    {
        Semanal,
        Quincenal,
        Mensual
    }

    public enum PaymentType
    {
        Efectivo,
        Debito,
        Credito,
        Otro
    }
}
