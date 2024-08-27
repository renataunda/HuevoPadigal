namespace PadigalAPI.DTOs
{
    public class AddressDto
    {
        public int Id { get; set; }

        public string AddressLine { get; set; }
        public string Neighborhood { get; set; }
        public string Zone { get; set; }
        public bool IsActive { get; set; }
    }
}
