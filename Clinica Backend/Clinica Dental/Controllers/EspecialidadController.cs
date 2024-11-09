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
    public class EspecialidadController : ControllerBase
    {
        private readonly ISvGeneric<Especialidad> _service;

        public EspecialidadController(ISvGeneric<Especialidad> service)
        {
            _service = service;
        }

        // GET: api/Especialidad
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EspecialidadPostDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            var dtoResult = result.Select(e => new EspecialidadPostDto
            {
                ID_Especialidad = e.ID_Especialidad,
                Nombre_Esp = e.Nombre_Esp,
                Descripcion_Esp = e.Descripcion_Esp
            }).ToList();

            return Ok(dtoResult);
        }

        // GET: api/Especialidad/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EspecialidadPostDto>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = new EspecialidadPostDto
            {
                ID_Especialidad = result.ID_Especialidad,
                Nombre_Esp = result.Nombre_Esp,
                Descripcion_Esp = result.Descripcion_Esp
            };

            return Ok(dtoResult);
        }

        // POST: api/Especialidad
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EspecialidadPostDto especialidadDto)
        {
            var especialidad = new Especialidad
            {
                ID_Especialidad = especialidadDto.ID_Especialidad,
                Nombre_Esp = especialidadDto.Nombre_Esp,
                Descripcion_Esp = especialidadDto.Descripcion_Esp
            };

            await _service.AddAsync(especialidad);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = especialidad.ID_Especialidad }, especialidadDto);
        }

        // PUT: api/Especialidad/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EspecialidadPostDto especialidadDto)
        {
            if (id != especialidadDto.ID_Especialidad)
            {
                return BadRequest();
            }

            var especialidad = new Especialidad
            {
                ID_Especialidad = especialidadDto.ID_Especialidad,
                Nombre_Esp = especialidadDto.Nombre_Esp,
                Descripcion_Esp = especialidadDto.Descripcion_Esp
            };

            await _service.UpdateAsync(especialidad);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Especialidad/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
