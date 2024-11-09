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
    public class Estado_CitaController : ControllerBase
    {
        private readonly ISvGeneric<Estado_Cita> _service;

        public Estado_CitaController(ISvGeneric<Estado_Cita> service)
        {
            _service = service;
        }

        // GET: api/Estado_Cita
        [HttpGet]
        public async Task<ActionResult<IEnumerable<EstadoCitasPostDto>>> Get()
        {
            var result = await _service.GetAllAsync();
            var dtoResult = result.Select(e => new EstadoCitasPostDto
            {
                ID_EstadoCita = e.ID_EstadoCita,
                Nombre_Estado = e.Nombre_Estado,
                Descripcion_Estado = e.Descripcion_Estado
            }).ToList();

            return Ok(dtoResult);
        }

        // GET: api/Estado_Cita/5
        [HttpGet("{id}")]
        public async Task<ActionResult<EstadoCitasPostDto>> Get(string id)
        {
            var result = await _service.GetByIdAsync(id);
            if (result == null)
            {
                return NotFound();
            }

            var dtoResult = new EstadoCitasPostDto
            {
                ID_EstadoCita = result.ID_EstadoCita,
                Nombre_Estado = result.Nombre_Estado,
                Descripcion_Estado = result.Descripcion_Estado
            };

            return Ok(dtoResult);
        }

        // POST: api/Estado_Cita
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] EstadoCitasPostDto estadoCitaDto)
        {
            var estadoCita = new Estado_Cita
            {
                ID_EstadoCita = estadoCitaDto.ID_EstadoCita,
                Nombre_Estado = estadoCitaDto.Nombre_Estado,
                Descripcion_Estado = estadoCitaDto.Descripcion_Estado
            };

            await _service.AddAsync(estadoCita);
            await _service.SaveChangesAsync();
            return CreatedAtAction(nameof(Get), new { id = estadoCita.ID_EstadoCita }, estadoCitaDto);
        }

        // PUT: api/Estado_Cita/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] EstadoCitasPostDto estadoCitaDto)
        {
            if (id != estadoCitaDto.ID_EstadoCita)
            {
                return BadRequest();
            }

            var estadoCita = new Estado_Cita
            {
                ID_EstadoCita = estadoCitaDto.ID_EstadoCita,
                Nombre_Estado = estadoCitaDto.Nombre_Estado,
                Descripcion_Estado = estadoCitaDto.Descripcion_Estado
            };

            await _service.UpdateAsync(estadoCita);
            await _service.SaveChangesAsync();
            return NoContent();
        }

        // DELETE: api/Estado_Cita/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _service.DeleteAsync(id);
            await _service.SaveChangesAsync();
            return NoContent();
        }
    }
}
