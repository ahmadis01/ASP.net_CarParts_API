using CarParts.Dto.StoreDto;
using CarParts.Repoistory.StoreRepository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;

namespace CarParts.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IStoreRepository _storeRepository;
        public StoreController(IStoreRepository storeRepository)
        {
            _storeRepository = storeRepository;
        }
        [HttpGet]
        public async Task<ActionResult> GetStores()
        {
            var stores = await _storeRepository.GetStores();
            return Ok(stores);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult> GetStore(int id)
        {
            var store = await _storeRepository.GetStore(id);
            return Ok(store);
        }
        [HttpPost]
        public async Task<ActionResult> AddStore(AddStoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var store = await _storeRepository.AddStore(storeDto);
            return Ok(store);
        }
        [HttpPut]
        public async Task<ActionResult> UpdateStore(UpdateStoreDto storeDto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);
            var store = await _storeRepository.UpdateStore(storeDto);
            return Ok(store);
        }
        [HttpDelete]
        public async Task<ActionResult> DeleteStore(int id)
        {
            var result = _storeRepository.DeleteStore(id);
            return Ok(result);
        }
    }
}
