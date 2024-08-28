using Microsoft.EntityFrameworkCore;
using PadigalAPI.Data;
using PadigalAPI.Models;

namespace PadigalAPI.Repositories
{
    public interface ISaleRepository
    {
        /// <summary>
        /// Retrieves a Sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the Sale to retrieve.</param>
        /// <returns>The Sale object.</returns>
        Task<Sale> GetSaleByIdAsync(int id);

        /// <summary>
        /// Creates a new Sale.
        /// </summary>
        /// <param name="sale">The Sale object to create.</param>
        /// <returns>The created Sale object.</returns>
        Task<Sale> CreateSaleAsync(Sale sale);

        /// <summary>
        /// Updates an existing Sale.
        /// </summary>
        /// <param name="sale">The Sale object to update.</param>
        Task UpdateSaleAsync(Sale sale);

        /// <summary>
        /// Retrieves all Sales.
        /// </summary>
        /// <returns>A list of all Sale objects.</returns>
        Task<IEnumerable<Sale>> GetAllSalesAsync();

        /// <summary>
        /// Deletes a Sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the Sale to delete.</param>
        /// <returns>A boolean indicating if the deletion was successful.</returns>
        Task<bool> DeleteSaleAsync(int id);
    }

    /// <inheritdoc />
    public class SaleRepository : ISaleRepository
    {
        private readonly PadigalContext _context;
        private readonly ILogger<SaleRepository> _logger;

        public SaleRepository(PadigalContext context, ILogger<SaleRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<Sale> GetSaleByIdAsync(int id)
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.Client)
                    .FirstOrDefaultAsync(s => s.Id == id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error retrieving sale with ID {id} from the database.");
                throw;
            }
        }

        public async Task<IEnumerable<Sale>> GetAllSalesAsync()
        {
            try
            {
                return await _context.Sales
                    .Include(s => s.Client)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales from the database.");
                throw;
            }
        }

        public async Task<Sale> CreateSaleAsync(Sale sale)
        {
            try
            {
                _context.Sales.Add(sale);
                await _context.SaveChangesAsync();
                return sale;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale in the database.");
                throw;
            }
        }

        public async Task UpdateSaleAsync(Sale sale)
        {
            try
            {
                _context.Sales.Update(sale);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error updating sale with ID {sale.Id} in the database.");
                throw;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                var sale = await _context.Sales.FindAsync(id);
                if (sale == null)
                {
                    return false;
                }

                _context.Sales.Remove(sale);
                await _context.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Error deleting sale with ID {id} from the database.");
                throw;
            }
        }
    }
}
