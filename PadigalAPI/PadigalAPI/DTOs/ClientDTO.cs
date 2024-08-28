using Newtonsoft.Json;
using PadigalAPI.Converters;
using PadigalAPI.Models;

namespace PadigalAPI.DTOs
{
    public class ClientDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Email { get; set; }
        public bool IsActive { get; set; }
        public string Notes { get; set; }

        [JsonConverter(typeof(ClientTypeConverter))]
        public ClientType ClientType { get; set; }
        public List<PhoneDto> PhoneNumbers { get; set; } = new List<PhoneDto>();
        public List<AddressDto> Addresses { get; set; } = new List<AddressDto>();
    }
}
