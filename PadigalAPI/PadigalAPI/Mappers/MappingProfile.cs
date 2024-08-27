using AutoMapper;
using PadigalAPI.DTOs;
using PadigalAPI.Models;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace PadigalAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<ClientPhone, PhoneDto>().ReverseMap();
            CreateMap<ClientAddress, AddressDto>().ReverseMap();
        }
    }

}
