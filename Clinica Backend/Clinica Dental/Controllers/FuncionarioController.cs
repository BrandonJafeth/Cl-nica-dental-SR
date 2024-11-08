using Application.GenericService;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FuncionarioController : ControllerBase
    {
        private readonly ISvGeneric<Funcionario> _service;

        public FuncionarioController(ISvGeneric<Funcionario> service)
        {
            _service = service;
        }

        // GET: api/Funcionario
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Funcionario>>> Get()
        {
            var result = await _service.GetAllAsync();
            return Ok(result);
        }

        // GET: api/Funcionario/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Funcionario>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }
            return Ok(result);
        }

        // POST: api/Funcionario
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] Funcionario funcionario)
        {
            await _service.AddAsync(funcionario);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = funcionario.ID_Funcionario }, funcionario);
        }

        // PUT: api/Funcionario/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] Funcionario funcionario)
        {
            if (id != funcionario.ID_Funcionario)
            {
                return BadRequest();
            }

            await _service.UpdateAsync(funcionario);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Funcionario/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
