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

        public ClientRepository(PadigalContext context)
        {
            _context = context;
        }

        public async Task<Client> CreateClientAsync(Client client)
        {
            _context.Clients.Add(client);
            await _context.SaveChangesAsync();
            return client;
        }

        public async Task<Client> GetClientByIdAsync(int id)
        {
            return await _context.Clients
                .Include(c => c.Addresses)
                .Include(c => c.PhoneNumbers)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task UpdateClientAsync(Client client)
        {
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
        }

       

        public async Task<IEnumerable<Client>> GetAllClientsAsync()
        {
            return await _context.Clients
                .Include(c => c.PhoneNumbers)
                .Include(c => c.Addresses)
                .ToListAsync();
        }

        public async Task<bool> DeleteClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            _context.Clients.Remove(client);
            await _context.SaveChangesAsync();
            return true;
        }
     
        public async Task<bool> DeactivateClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            client.IsActive = false;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
        public async Task<bool> ActivateClientAsync(int id)
        {
            var client = await _context.Clients.FindAsync(id);
            if (client == null) return false;

            client.IsActive = true;
            _context.Clients.Update(client);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
