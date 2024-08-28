using Microsoft.AspNetCore.Mvc;
using PadigalAPI.DTOs;
using PadigalAPI.Exceptions;
using PadigalAPI.Services;
using System.Net;

namespace PadigalAPI.Controllers
{
    /// <summary>
    /// api/clients
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;
        private readonly ILogger<ClientsController> _logger;

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="clientService">The client service used for client operations.</param>
        /// <param name="logger">The logger for logging information and errors.</param>
        public ClientsController(IClientService clientService, ILogger<ClientsController> logger)
        {
            _clientService = clientService;
            _logger = logger;
        }

        /// <summary>
        /// Retrieves client data (including phones and addresses) by the specified id.
        /// </summary>
        /// <param name="id">The id of the client to retrieve.</param>
        /// <returns>The DTO containing the client data.</returns>
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            try
            {
                var client = await _clientService.GetClientByIdAsync(id);
                return Ok(client);
            }
            catch (NotFoundException ex)
            {
                _logger.LogWarning(ex, "Client not found with ID {Id}", id);
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving client with ID {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Creates a new client with the provided data.
        /// </summary>
        /// <param name="clientDto">The DTO containing the client data to create.</param>
        /// <returns>The created client DTO with the assigned ID.</returns>
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
            {
                _logger.LogWarning("Attempted to create a client with null data.");
                return BadRequest(new { message = "Client data cannot be null" });
            }

            try
            {
                var client = await _clientService.CreateClientAsync(clientDto);
                return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while creating a client.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Deletes a client by the specified id.
        /// </summary>
        /// <param name="id">The id of the client to delete.</param>
        /// <returns>No content if successful, otherwise a not found or error result.</returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            try
            {
                var result = await _clientService.DeleteClientAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Attempted to delete non-existing client with ID {Id}", id);
                    return NotFound(new { message = "Client not found" });
                }
                return Ok(result);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deleting client with ID {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Updates an existing client with the provided data.
        /// </summary>
        /// <param name="id">The id of the client to update.</param>
        /// <param name="clientDto">The DTO containing the updated client data.</param>
        /// <returns>The updated client DTO, or a not found or error result.</returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            if (id != clientDto.Id)
            {
                _logger.LogWarning("Client ID mismatch: received ID {Id} does not match DTO ID {DtoId}", id, clientDto.Id);
                return BadRequest("Client ID mismatch.");
            }

            try
            {
                var updatedClient = await _clientService.UpdateClientAsync(clientDto);
                if (updatedClient == null)
                {
                    _logger.LogWarning("Attempted to update non-existing client with ID {Id}", id);
                    return NotFound(new { message = "Client not found" });
                }
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while updating client with ID {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a list of all clients.
        /// </summary>
        /// <returns>A list of client DTOs.</returns>
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            try
            {
                var clients = await _clientService.GetAllClientsAsync();
                return Ok(clients);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while retrieving all clients.");
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Deactivates a client by the specified id.
        /// </summary>
        /// <param name="id">The id of the client to deactivate.</param>
        /// <returns>No content if successful, otherwise a not found or error result.</returns>
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateClient(int id)
        {
            try
            {
                var result = await _clientService.DeactivateClientAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Attempted to deactivate non-existing client with ID {Id}", id);
                    return NotFound(new { message = "Client not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while deactivating client with ID {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }

        /// <summary>
        /// Activates a client by the specified id.
        /// </summary>
        /// <param name="id">The id of the client to activate.</param>
        /// <returns>No content if successful, otherwise a not found or error result.</returns>
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateClient(int id)
        {
            try
            {
                var result = await _clientService.ActivateClientAsync(id);
                if (!result)
                {
                    _logger.LogWarning("Attempted to activate non-existing client with ID {Id}", id);
                    return NotFound(new { message = "Client not found" });
                }
                return NoContent();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An unexpected error occurred while activating client with ID {Id}", id);
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }
    }
}
