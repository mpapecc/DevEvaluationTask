using Contacts.Api.Models;
using Contacts.Api.Repositories.Abstraction;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Contacts.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public abstract class BaseController<T> : ControllerBase where T : BaseEntity
    {
        private IBaseRepository<T> _baseRepository;
        public BaseController(IBaseRepository<T> baseRepository)
        {
            _baseRepository = baseRepository;
        }

        [HttpGet]
        public virtual async Task<IActionResult> GetAll()
        {
            return Ok(await _baseRepository.GetAll().AsNoTracking().ToListAsync());
        }

        [HttpGet("{id}")]
        public virtual async Task<IActionResult> Find(Guid id)
        {
            var record =  await _baseRepository.GetById(id).AsNoTracking().FirstOrDefaultAsync();
            if (record == null)
                return NotFound();

            return Ok(record);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] T record)
        {
            if (await _baseRepository.AddAsync(record))
                return Ok(record); 
            return BadRequest();
            
        }

        [HttpPut("{id}")]
        public virtual async Task<IActionResult> Update(Guid id, [FromBody] T record)
        {
            if (id != record.Id)
                return BadRequest();

            
            if (await _baseRepository.UpdateAsync(record))
                return Ok(record);
            return BadRequest();
            
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            
            if (await _baseRepository.DeleteAsync(id))
                return NoContent();
            return BadRequest();
            
        }
    }
}
