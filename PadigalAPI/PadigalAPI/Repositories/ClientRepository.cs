using Microsoft.EntityFrameworkCore;
using PadigalAPI.Data;
using PadigalAPI.Models;
namespace PadigalAPI.Repositories
{
    public interface IClientRepository
    {
        /// <summary>
        /// Retrieves a client by its ID.
        /// </summary>
        /// <param name="id">The ID of the client.</param>
        /// <returns>The client entity.</returns>
        Task<Client> GetClientByIdAsync(int id);

        /// <summary>
        /// Creates a new client in the database.
        /// </summary>
        /// <param name="client">The client entity to create.</param>
        /// <returns>The created client entity.</returns>
        Task<Client> CreateClientAsync(Client client);

        /// <summary>
        /// Updates an existing client in the database.
        /// </summary>
        /// <param name="client">The client entity with updated information.</param>
        Task UpdateClientAsync(Client client);

        /// <summary>
        /// Retrieves all clients from the database.
        /// </summary>
        /// <returns>A list of all client entities.</returns>
        Task<IEnumerable<Client>> GetAllClientsAsync();

        /// <summary>
        /// Deletes a client by its ID from the database.
        /// </summary>
        /// <param name="id">The ID of the client to delete.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> DeleteClientAsync(int id);

        /// <summary>
        /// Deactivates a client by its ID in the database.
        /// </summary>
        /// <param name="id">The ID of the client to deactivate.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> DeactivateClientAsync(int id);

        /// <summary>
        /// Activates a client by its ID in the database.
        /// </summary>
        /// <param name="id">The ID of the client to activate.</param>
        /// <returns>A boolean indicating success or failure.</returns>
        Task<bool> ActivateClientAsync(int id);
    }

    /// <inheritdoc />
    public class ClientRepository : IClientRepository
    {
        private readonly PadigalContext _context;
        private readonly ILogger<ClientRepository> _logger;

        public ClientRepository(PadigalContext context, ILogger<ClientRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            try
            {
                _context.Clients.Add(client);
                await _context.SaveChangesAsync();
                return client;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating client in database");
                throw;
            }
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            try
            {
                return await _context.Clients
                    .Include(c => c.Addresses)
                    .Include(c => c.PhoneNumbers)
                    .FirstOrDefaultAsync(c => c.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting client by ID from database");
                throw;
            }
        }

        public async Task UpdateClientAsync(Client client)
        {
            try
            {
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating client in database");
                throw;
            }
        }

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            try
            {
                return await _context.Clients
                    .Include(c => c.PhoneNumbers)
                    .Include(c => c.Addresses)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all clients from database");
                throw;
            }
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) return false;

                _context.Clients.Remove(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting client from database");
                throw;
            }
        }

        public async Task<bool> DeactivateClientAsync(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) return false;

                client.IsActive = false;
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deactivating client in database");
                throw;
            }
        }

        public async Task<bool> ActivateClientAsync(int id)
        {
            try
            {
                var client = await _context.Clients.FindAsync(id);
                if (client == null) return false;

                client.IsActive = true;
                _context.Clients.Update(client);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error activating client in database");
                throw;
            }
        }
    }
}
