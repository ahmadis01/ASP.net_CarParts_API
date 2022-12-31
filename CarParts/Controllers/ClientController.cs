using CarParts.Dto.ClientDto;
using CarParts.Repoistory.ClientRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ClientController : ControllerBase
    {
        private readonly IClientRepository clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            this.clientRepository = clientRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetClients()
        {
            var clients = await clientRepository.GetClients();
            return Ok(clients);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetClient(int id)
        {
            var client = await clientRepository.GetClient(id);
            return Ok(client);
        }
        [HttpGet("/api/Client/GetClientByName/{name}")]
        public async Task<ActionResult> GetClient(string name)
        {
            var client = await clientRepository.GetClient(name);
            return Ok(client);
        }
        [HttpPost]
        public async Task<ActionResult> AddClient(AddClientDto clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var client = await clientRepository.AddClient(clientDto);
            return Ok(client);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateClient(UpdateClientDto clientDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var client = await clientRepository.UpdateClient(clientDto);
            return Ok(client);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteClient(int id)
        {
            var result = clientRepository.DeleteClient(id);
            return Ok(result);
        }
    }
}
