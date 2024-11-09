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
    public class Estado_TratamientoController : ControllerBase
    {
        private readonly ISvGeneric<Estado_Tratamiento> _service;

        public Estado_TratamientoController(ISvGeneric<Estado_Tratamiento> service)
        {
            _service = service;
        }

        // GET: api/Estado_Tratamiento
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoTratamientoPostDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            var dtoResult = result.Select(e => new EstadoTratamientoPostDto
            {
                ID_EstadoTratamiento = e.ID_EstadoTratamiento,
                Nombre_Estado = e.Nombre_Estado,
                Descripcion_Estado = e.Descripcion_Estado
            }).ToList();

            return Ok(dtoResult);
        }

        // GET: api/Estado_Tratamiento/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoTratamientoPostDto>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = new EstadoTratamientoPostDto
            {
                ID_EstadoTratamiento = result.ID_EstadoTratamiento,
                Nombre_Estado = result.Nombre_Estado,
                Descripcion_Estado = result.Descripcion_Estado
            };

            return Ok(dtoResult);
        }

        // POST: api/Estado_Tratamiento
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstadoTratamientoPostDto estadoTratamientoDto)
        {
            var estadoTratamiento = new Estado_Tratamiento
            {
                ID_EstadoTratamiento = estadoTratamientoDto.ID_EstadoTratamiento,
                Nombre_Estado = estadoTratamientoDto.Nombre_Estado,
                Descripcion_Estado = estadoTratamientoDto.Descripcion_Estado
            };

            await _service.AddAsync(estadoTratamiento);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estadoTratamiento.ID_EstadoTratamiento }, estadoTratamientoDto);
        }

        // PUT: api/Estado_Tratamiento/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EstadoTratamientoPostDto estadoTratamientoDto)
        {
            if (id != estadoTratamientoDto.ID_EstadoTratamiento)
            {
                return BadRequest();
            }

            var estadoTratamiento = new Estado_Tratamiento
            {
                ID_EstadoTratamiento = estadoTratamientoDto.ID_EstadoTratamiento,
                Nombre_Estado = estadoTratamientoDto.Nombre_Estado,
                Descripcion_Estado = estadoTratamientoDto.Descripcion_Estado
            };

            await _service.UpdateAsync(estadoTratamiento);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Estado_Tratamiento/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
