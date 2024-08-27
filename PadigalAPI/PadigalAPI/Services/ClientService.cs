using AutoMapper;
using PadigalAPI.DTOs;
using PadigalAPI.Exceptions;
using PadigalAPI.Models;
using PadigalAPI.Repositories;

namespace PadigalAPI.Services
{
    /// <summary>
    /// Interface for managing client operations.
    /// </summary>
    public interface IClientService
    {
        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="clientDto">The client data transfer object.</param>
        /// <returns>The created client data transfer object.</returns>
        Task<ClientDto> CreateClientAsync(ClientDto clientDto);

        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>The client data transfer object.</returns>
        Task<ClientDto> GetClientByIdAsync(int id);

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="clientDto">The client data transfer object with updated information.</param>
        /// <returns>The updated client data transfer object.</returns>
        Task<ClientDto> UpdateClientAsync(ClientDto clientDto);

        /// <summary>
        /// Retrieves all clients.
        /// </summary>
        /// <returns>A list of all client data transfer objects.</returns>
        Task<IEnumerable<ClientDto>> GetAllClientsAsync();

        /// <summary>
        /// Deletes a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be deleted.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> DeleteClientAsync(int id);

        /// <summary>
        /// Deactivates a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be deactivated.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> DeactivateClientAsync(int id);

        /// <summary>
        /// Activates a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be activated.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> ActivateClientAsync(int id);
    }

    /// <summary>
    /// Service implementation for managing client operations.
    /// </summary>
    public class ClientService : IClientService
    {
        private readonly IClientRepository _clientRepository;
        private readonly ILogger<ClientService> _logger;
        private readonly IMapper _mapper;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientService"/> class.
        /// </summary>
        /// <param name="clientRepository">The repository for client data access.</param>
        /// <param name="logger">The logger instance for logging errors.</param>
        /// <param name="mapper">The mapper for converting between entities and DTOs.</param>
        public ClientService(IClientRepository clientRepository, ILogger<ClientService> logger, IMapper mapper)
        {
            _clientRepository = clientRepository;
            _mapper = mapper;
            _logger = logger;
        }

        /// <summary>
        /// Creates a new client.
        /// </summary>
        /// <param name="clientDto">The client data transfer object.</param>
        /// <returns>The created client data transfer object.</returns>
        public async Task<ClientDto> CreateClientAsync(ClientDto clientDto)
        {
            try
            {
                var client = _mapper.Map<Client>(clientDto);
                await _clientRepository.CreateClientAsync(client);
                return _mapper.Map<ClientDto>(client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating client");
                throw;
            }
        }

        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>The client data transfer object.</returns>
        public async Task<ClientDto> GetClientByIdAsync(int id)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(id);
                if (client == null)
                {
                    throw new NotFoundException("Client", id, $"Client with ID {id} not found");
                }
                return _mapper.Map<ClientDto>(client);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting client by ID");
                throw;
            }
        }

        /// <summary>
        /// Updates an existing client.
        /// </summary>
        /// <param name="clientDto">The client data transfer object with updated information.</param>
        /// <returns>The updated client data transfer object.</returns>
        public async Task<ClientDto> UpdateClientAsync(ClientDto clientDto)
        {
            try
            {
                var client = await _clientRepository.GetClientByIdAsync(clientDto.Id);
                if (client == null)
                {
                    throw new NotFoundException("Client", clientDto.Id, $"Client with ID {clientDto.Id} not found");
                }

                _mapper.Map(clientDto, client);
                await _clientRepository.UpdateClientAsync(client);
                return _mapper.Map<ClientDto>(client);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating client");
                throw;
            }
        }

        /// <summary>
        /// Retrieves all clients.
        /// </summary>
        /// <returns>A list of all client data transfer objects.</returns>
        public async Task<IEnumerable<ClientDto>> GetAllClientsAsync()
        {
            try
            {
                var clients = await _clientRepository.GetAllClientsAsync();
                return _mapper.Map<IEnumerable<ClientDto>>(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all clients");
                throw;
            }
        }

        /// <summary>
        /// Deletes a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be deleted.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                return await _clientRepository.DeleteClientAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting client");
                throw;
            }
        }

        /// <summary>
        /// Deactivates a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be deactivated.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        public async Task<bool> DeactivateClientAsync(int id)
        {
            try
            {
                return await _clientRepository.DeactivateClientAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating client");
                throw;
            }
        }

        /// <summary>
        /// Activates a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client to be activated.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        public async Task<bool> ActivateClientAsync(int id)
        {
            try
            {
                return await _clientRepository.ActivateClientAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating client");
                throw;
            }
        }
    }
}
