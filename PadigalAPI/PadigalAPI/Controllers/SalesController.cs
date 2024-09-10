using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using PadigalAPI.DTOs;
using PadigalAPI.Services;

namespace PadigalAPI.Controllers
{
    /// <summary>
    /// api/sales
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly ISaleService _saleService;
        private readonly ILogger<SalesController> _logger;

        public SalesController(ISaleService saleService, ILogger<SalesController> logger)
        {
            _saleService = saleService;
            _logger = logger;
        }

        /// <summary>
        /// Gets a sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the sale.</param>
        /// <returns>The sale details.</returns>
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetSaleById(int id)
        {
            try
            {
                var sale = await _saleService.GetSaleByIdAsync(id);
                if (sale == null)
                {
                    return NotFound();
                }
                return Ok(sale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving sale by ID");
                return NotFound("Sale not found.");
            }
        }


        /// <summary>
        /// Creates a new sale.
        /// </summary>
        /// <param name="saleDto">The sale details.</param>
        /// <returns>The created sale.</returns>
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> CreateSale([FromBody] SaleDto saleDto)
        {
            if (saleDto == null)
            {
                return BadRequest(new { message = "Order data cannot be null" });
            }

            try
            {
                var createdSale = await _saleService.CreateSaleAsync(saleDto);
                return CreatedAtAction(nameof(GetSaleById), new { id = createdSale.Id }, createdSale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating sale");
                return BadRequest("An error occurred while creating the sale.");
            }
        }


        /// <summary>
        /// Deletes a sale by its ID.
        /// </summary>
        /// <param name="id">The ID of the sale to delete.</param>
        /// <returns>A boolean indicating whether the deletion was successful.</returns>
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteSale(int id)
        {
            try
            {
                var result = await _saleService.DeleteSaleAsync(id);
                if (!result) return NotFound(new { message = "Order not found" });
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting sale");
                return BadRequest("An error occurred while deleting the sale.");
            }
        }

        /// <summary>
        /// Updates an existing sale.
        /// </summary>
        /// <param name="saleDto">The updated sale details.</param>
        /// <returns>The updated sale.</returns>
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> UpdateSale(int id, [FromBody] SaleDto saleDto)
        {
            if (id != saleDto.Id)
            {
                return BadRequest("Order ID mismatch.");
            }

            try
            {
                var updatedSale = await _saleService.UpdateSaleAsync(saleDto);
                if (updatedSale == null)
                {
                    return NotFound();
                }
                return Ok(updatedSale);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating sale");
                return BadRequest("An error occurred while updating the sale.");
            }
        }

        /// <summary>
        /// Retrieves all sales.
        /// </summary>
        /// <returns>A collection of sales.</returns>
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllSales()
        {
            try
            {
                var sales = await _saleService.GetAllSalesAsync();
                return Ok(sales);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving all sales");
                return StatusCode(StatusCodes.Status500InternalServerError, "An error occurred while retrieving the sales.");
            }
        }
    }
}
