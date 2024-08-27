using AutoMapper;
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
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();
        Task<bool> DeleteClientAsync(int id);
        Task<bool> DeactivateClientAsync(int id);
        Task<bool> ActivateClientAsync(int id);

    }

    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly IMapper _mapper;

        public ClientService(IClientRepository clientRepository, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
        }
        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            var client = _mapper.Map<Client>(clientDto);
            await _clientRepository.CreateClientAsync(client);
            return _mapper.Map<ClientDto>(client);
        } 

        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            var client = await _clientRepository.GetClientByIdAsync(id);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto)
        {
            var client = await _clientRepository.GetClientByIdAsync(clientDto.Id);
            if (client == null)
            {
                return null;
            }

            _mapper.Map(clientDto, client);
            await _clientRepository.UpdateClientAsync(client);
            return _mapper.Map<ClientDto>(client);
        }

        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            var clients = await _clientRepository.GetAllClientsAsync();
            return _mapper.Map<IEnumerable<ClientDto>>(clients);
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            return await _clientRepository.DeleteClientAsync(id);
        }

        public async Task<bool> DeactivateClientAsync(int id)
        {
            return await _clientRepository.DeactivateClientAsync(id);
        }

        public async Task<bool> ActivateClientAsync(int id)
        {
            return await _clientRepository.ActivateClientAsync(id);
        }
    }
}
