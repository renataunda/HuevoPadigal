using PadigalAPI.DTOs;
using PadigalAPI.Models;
using PadigalAPI.Repositories;
using System.Threading.Tasks;

namespace PadigalAPI.Services
{
    public interface IClientService
    {
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);
        Task<ClientDto> GetClientByIdAsync(int id);
        Task<ClientDto> UpdateClientAsync(ClientDto clientDto);
        Task DeleteClientAsync(int id);
    }

    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;

        public ClientService(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = new Client
            {
                Name = clientDto.Name,
                Email = clientDto.Email,
                IsActive = clientDto.IsActive,
                Notes = clientDto.Notes,
                ClientType = clientDto.ClientType,
                PhoneNumbers = clientDto.Phones.Select(p => new ClientPhone
                {
                    PhoneNumber = p.PhoneNumber,
                    IsActive = p.IsActive
                }).ToList(),
                Addresses = clientDto.Addresses.Select(a => new ClientAddress
                {
                    AddressLine = a.AddressLine,
                    Neighborhood = a.Neighborhood,
                    Zone = a.Zone,
                    IsActive = a.IsActive
                }).ToList()
            };

            await _clientRepository.CreateClientAsync(client);
            return clientDto; // Consider adding mapping here if necessary
        }

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            // Map the client to a DTO here
            return new ClientDto
            {
                Id = client.Id,
                Name = client.Name,
                Email = client.Email,
                IsActive = client.IsActive,
                Notes = client.Notes,
                ClientType = client.ClientType,
                Phones = client.PhoneNumbers.Select(p => new PhoneDto
                {
                    Id = p.Id,
                    PhoneNumber = p.PhoneNumber,
                    IsActive = p.IsActive
                }).ToList(),
                Addresses = client.Addresses.Select(a => new AddressDto
                {
                    Id = a.Id,
                    AddressLine = a.AddressLine,
                    Neighborhood = a.Neighborhood,
                    Zone = a.Zone,
                    IsActive = a.IsActive
                }).ToList()
            };
        }

        public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientDto.Id);
            if (client == null)
            {
                return null;
            }

            client.Name = clientDto.Name;
            client.Email = clientDto.Email;
            client.IsActive = clientDto.IsActive;
            client.Notes = clientDto.Notes;
            client.ClientType = clientDto.ClientType;

            foreach (var phoneDto in clientDto.Phones)
            {
                var existingPhone = client.PhoneNumbers.FirstOrDefault(p => p.Id == phoneDto.Id);
                if (existingPhone != null)
                {
                    existingPhone.PhoneNumber = phoneDto.PhoneNumber;
                    existingPhone.IsActive = phoneDto.IsActive;
                }
                else
                {
                    client.PhoneNumbers.Add(new ClientPhone
                    {
                        PhoneNumber = phoneDto.PhoneNumber,
                        IsActive = phoneDto.IsActive
                    });
                }
            }

            foreach (var addressDto in clientDto.Addresses)
            {
                var existingAddress = client.Addresses.FirstOrDefault(a => a.Id == addressDto.Id);
                if (existingAddress != null)
                {
                    existingAddress.AddressLine = addressDto.AddressLine;
                    existingAddress.Neighborhood = addressDto.Neighborhood;
                    existingAddress.Zone = addressDto.Zone;
                    existingAddress.IsActive = addressDto.IsActive;
                }
                else
                {
                    client.Addresses.Add(new ClientAddress
                    {
                        AddressLine = addressDto.AddressLine,
                        Neighborhood = addressDto.Neighborhood,
                        Zone = addressDto.Zone,
                        IsActive = addressDto.IsActive
                    });
                }
            }

            await _clientRepository.UpdateClientAsync(client);
            // Map the updated client to a DTO here
            return clientDto;
        }

        public async Task DeleteClientAsync(int id)
        {
            await _clientRepository.DeleteClientAsync(id);
        }
    }
}
