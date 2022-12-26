using CarParts.Dto.PartDto;
using CarParts.Repoistory.PartRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PartController : ControllerBase
    {
        private readonly IPartRepository _partRepository;

        public PartController(IPartRepository partRepository)
        {
            _partRepository = partRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetParts()
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var parts = await _partRepository.GetParts();
            return Ok(parts);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetPart(int id)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var part = await _partRepository.GetPart(id);
            return Ok(part);
        }
        [HttpGet("/api/Part/GetPartByName")]
        public async Task<ActionResult> GetPartByName(string name)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var part = await _partRepository.GetPart(name);
            return Ok(part);
        }
        [HttpPost]
        public async Task<ActionResult> AddPart([FromForm] AddPartDto partDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var part =await _partRepository.AddPart(partDto);
            return Ok(part);
        }
        [HttpPut]
        public async Task<ActionResult> UpdatePart([FromForm] UpdatePartDto partDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var part = await _partRepository.UpdatePart(partDto);
            return Ok(part);
        }
        [HttpDelete]
        public async Task<ActionResult> DeletePart(int id)
        {
            var result = _partRepository.DeletePart(id);
            return Ok(result);

        }
    }
}
