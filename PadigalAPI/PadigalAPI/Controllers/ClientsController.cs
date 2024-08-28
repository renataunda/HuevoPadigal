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

        /// <summary>
        /// Initializes a new instance of the <see cref="ClientsController"/> class.
        /// </summary>
        /// <param name="clientService">The client service used for client operations.</param>
        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
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
                return NotFound(new { message = ex.Message });
            }
            catch (Exception ex)
            {
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
                return BadRequest(new { message = "Client data cannot be null" });
            }

            try
            {
                var client = await _clientService.CreateClientAsync(clientDto);
                return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
            }
            catch (Exception ex)
            {
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
                if (!result) return NotFound(new { message = "Client not found" });
                return Ok(result);
            }
            catch (Exception ex)
            {
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
                return BadRequest("Client ID mismatch.");
            }

            try
            {
                var updatedClient = await _clientService.UpdateClientAsync(clientDto);
                if (updatedClient == null) return NotFound(new { message = "Client not found" });
                return Ok(updatedClient);
            }
            catch (Exception ex)
            {
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
                if (!result) return NotFound(new { message = "Client not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
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
                if (!result) return NotFound(new { message = "Client not found" });
                return NoContent();
            }
            catch (Exception ex)
            {
                return StatusCode((int)HttpStatusCode.InternalServerError, new { message = "An unexpected error occurred.", detailed = ex.Message });
            }
        }
    }
}
