using Application.Dtos.PostDtos;
using Application.GenericService;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoCuentaController : ControllerBase
    {
        private readonly ISvGeneric<Estado_Cuenta> _service;

        public EstadoCuentaController(ISvGeneric<Estado_Cuenta> service)
        {
            _service = service;
        }

        // GET: api/EstadoCuenta
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoCuentaPostDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            var dtoResult = result.Select(e => new EstadoCuentaPostDto
            {
                ID_Estado_Cuenta = e.ID_Estado_Cuenta,
                Nombre_EC = e.Nombre_EC,
                Descripcion_EC = e.Descripcion_EC
            }).ToList();

            return Ok(dtoResult);
        }

        // GET: api/EstadoCuenta/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCuentaPostDto>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = new EstadoCuentaPostDto
            {
                ID_Estado_Cuenta = result.ID_Estado_Cuenta,
                Nombre_EC = result.Nombre_EC,
                Descripcion_EC = result.Descripcion_EC
            };

            return Ok(dtoResult);
        }

        // POST: api/EstadoCuenta
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstadoCuentaPostDto estadoCuentaDto)
        {
            var estadoCuenta = new Estado_Cuenta
            {
                ID_Estado_Cuenta = estadoCuentaDto.ID_Estado_Cuenta,
                Nombre_EC = estadoCuentaDto.Nombre_EC,
                Descripcion_EC = estadoCuentaDto.Descripcion_EC
            };

            await _service.AddAsync(estadoCuenta);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estadoCuenta.ID_Estado_Cuenta }, estadoCuentaDto);
        }

        // PUT: api/EstadoCuenta/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EstadoCuentaPostDto estadoCuentaDto)
        {
            if (id != estadoCuentaDto.ID_Estado_Cuenta)
            {
                return BadRequest();
            }

            var estadoCuenta = new Estado_Cuenta
            {
                ID_Estado_Cuenta = estadoCuentaDto.ID_Estado_Cuenta,
                Nombre_EC = estadoCuentaDto.Nombre_EC,
                Descripcion_EC = estadoCuentaDto.Descripcion_EC
            };

            await _service.UpdateAsync(estadoCuenta);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/EstadoCuenta/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
