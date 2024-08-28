using AutoMapper;
using PadigalAPI.DTOs;
using PadigalAPI.Models;

namespace PadigalAPI.Mappers
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Client, ClientDto>().ReverseMap();
            CreateMap<ClientPhone, PhoneDto>().ReverseMap();
            CreateMap<ClientAddress, AddressDto>().ReverseMap();
            CreateMap<Sale, SaleDto>().ReverseMap();
        }
    }

}
