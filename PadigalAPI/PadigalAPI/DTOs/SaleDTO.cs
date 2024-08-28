using Newtonsoft.Json;
using PadigalAPI.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.DTOs
{
    public class SaleDto
    {
        public int Id { get; set; }
        public DateTime OrderDate { get; set; }
        public DateTime DeliveryDate { get; set; }

        [JsonConverter(typeof(ProductTypeConverter))]
        public ProductType ProductType { get; set; }
        public int Quantity { get; set; }
        public decimal UnitPrice { get; set; }
        public decimal TotalPrice { get; set; }
        public bool Recurring { get; set; }

        [JsonConverter(typeof(FrequencyConverter))]
        public Frequency? Frequency { get; set; }
        public List<decimal>? BoxWeights { get; set; }
        public decimal TotalWeight { get; set; }

        [JsonConverter(typeof(PaymentTypeConverter))]
        public PaymentType? PaymentType { get; set; }
        public bool IsPaid { get; set; }
        public string Notes { get; set; }
        public int ClientId { get; set; }
        public int PhoneNumberId { get; set; }
        public int AddressId { get; set; }
    }
}
