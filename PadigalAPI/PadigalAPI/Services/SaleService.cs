using AutoMapper;
using PadigalAPI.DTOs;
using PadigalAPI.Exceptions;
using PadigalAPI.Models;
using PadigalAPI.Repositories;

namespace PadigalAPI.Services
{
    public interface ISaleService
    {
        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="saleDto">The sale data transfer object.</param>
        /// <returns>The created sale.</returns>
        Task<SaleDto> CreateSaleAsync(SaleDto saleDto);

        /// <summary>
        /// Gets a sale by its ID.
        /// </summary>
        /// <param name="id">The sale ID.</param>
        /// <returns>The sale with the specified ID.</returns>
        Task<SaleDto> GetSaleByIdAsync(int id);

        /// <summary>
        /// Updates an existing sale.
        /// </summary>
        /// <param name="saleDto">The sale data transfer object.</param>
        /// <returns>The updated sale.</returns>
        Task<SaleDto> UpdateSaleAsync(SaleDto saleDto);

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>A collection of sales.</returns>
        Task<IEnumerable<SaleDto>> GetAllSalesAsync();

        /// <summary>
        /// Deletes a sale by its ID.
        /// </summary>
        /// <param name="id">The sale ID.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        Task<bool> DeleteSaleAsync(int id);
    }

    /// <inheritdoc />
    public class SaleService : ISaleService
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<SaleService> _logger;

        public SaleService(ISaleRepository saleRepository, IMapper mapper, ILogger<SaleService> logger)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<SaleDto> CreateSaleAsync(SaleDto saleDto)
        {
            try
            {
                var sale = _mapper.Map<Sale>(saleDto);
                await _saleRepository.CreateSaleAsync(sale);
                return _mapper.Map<SaleDto>(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                throw;
            }
        }

        public async Task<SaleDto> GetSaleByIdAsync(int id)
        {
            try
            {
                var sale = await _saleRepository.GetSaleByIdAsync(id);
                if (sale == null)
                {
                    throw new NotFoundException("Sale", id, $"Sale with ID {id} not found");
                }
                return _mapper.Map<SaleDto>(sale);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Sale not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting sale by ID");
                throw;
            }
        }

        public async Task<SaleDto> UpdateSaleAsync(SaleDto saleDto)
        {
            try
            {
                var sale = await _saleRepository.GetSaleByIdAsync(saleDto.Id);
                if (sale == null)
                {
                    throw new NotFoundException("Sale", saleDto.Id, $"Sale with ID {saleDto.Id} not found");
                }

                _mapper.Map(saleDto, sale);
                await _saleRepository.UpdateSaleAsync(sale);
                return _mapper.Map<SaleDto>(sale);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Sale not found");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale");
                throw;
            }
        }

        public async Task<IEnumerable<SaleDto>> GetAllSalesAsync()
        {
            try
            {
                var sales = await _saleRepository.GetAllSalesAsync();
                return _mapper.Map<IEnumerable<SaleDto>>(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales");
                throw;
            }
        }

        public async Task<bool> DeleteSaleAsync(int id)
        {
            try
            {
                return await _saleRepository.DeleteSaleAsync(id);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale");
                throw;
            }
        }
    }
}
