using Microsoft.EntityFrameworkCore;
using PadigalAPI.Data;
using PadigalAPI.Models;

namespace PadigalAPI.Repositories
{
    public interface IClientRepository
    {
        Task<Client> GetClientByIdAsync(int id);
        Task<Client> CreateClientAsync(Client client);
        Task UpdateClientAsync(Client client);
        Task<IEnumerable<Client>> GetAllClientsAsync();
        Task<bool> DeleteClientAsync(int id);
        Task<bool> DeactivateClientAsync(int id);
        Task<bool> ActivateClientAsync(int id);
    }

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
