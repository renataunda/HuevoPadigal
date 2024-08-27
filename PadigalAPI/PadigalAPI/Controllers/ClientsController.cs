using Microsoft.AspNetCore.Mvc;
using PadigalAPI.DTOs;
using PadigalAPI.Services;
using System.Net;

namespace PadigalAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ClientsController : ControllerBase
    {
        private readonly IClientService _clientService;

        public ClientsController(IClientService clientService)
        {
            _clientService = clientService;
        }

        // GET: api/clients/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ClientDto>> GetClient(int id)
        {
            var client = await _clientService.GetClientByIdAsync(id);

            if (client == null)
            {
                return NotFound();
            }

            return Ok(client);
        }

        // POST: api/clients
        [HttpPost]
        public async Task<IActionResult> CreateClient([FromBody] ClientDto clientDto)
        {
            if (clientDto == null)
            {
                return BadRequest();
            }

            var client = await _clientService.CreateClientAsync(clientDto);
            return CreatedAtAction(nameof(GetClient), new { id = client.Id }, client);
        }

        // DELETE: api/clients/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteClient(int id)
        {
            var result = await _clientService.DeleteClientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // PUT: api/clients/5
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateClient(int id, [FromBody] ClientDto clientDto)
        {
            if (id != clientDto.Id)
            {
                return BadRequest("Client ID mismatch.");
            }

            var updatedClient = await _clientService.UpdateClientAsync(clientDto);

            if (updatedClient == null)
            {
                return NotFound();
            }

            return Ok(updatedClient);
        }

        // GET: api/clients
        [HttpGet]
        public async Task<IActionResult> GetAllClients()
        {
            var clients = await _clientService.GetAllClientsAsync();
            return Ok(clients);
        }

        // PUT: api/clients/5/deactivate
        [HttpPut("{id}/deactivate")]
        public async Task<IActionResult> DeactivateClient(int id)
        {
            var result = await _clientService.DeactivateClientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

        // PUT: api/clients/5/activate
        [HttpPut("{id}/activate")]
        public async Task<IActionResult> ActivateClient(int id)
        {
            var result = await _clientService.ActivateClientAsync(id);
            if (!result) return NotFound();
            return NoContent();
        }

    }





}
