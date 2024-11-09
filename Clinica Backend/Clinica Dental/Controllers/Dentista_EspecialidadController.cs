using Application.BrandonServices;
using Application.Dtos.PostDtos;
using Domain.Interfaces.Brandon_Interfaces;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class Dentista_EspecialidadController : ControllerBase
    {
        private readonly ISvDentista_Especialidad _svDentistaEspecialidad;

        public Dentista_EspecialidadController(ISvDentista_Especialidad svDentistaEspecialidad)
        {
            _svDentistaEspecialidad = svDentistaEspecialidad;
        }

        // GET: api/<Dentista_EspecialidadController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<DentistaEspecialidadDto>>> Get()
        {
            var dentistaEspecialidades = await _svDentistaEspecialidad.GetAllAsync();
            return Ok(dentistaEspecialidades);
        }

        // GET api/<Dentista_EspecialidadController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<DentistaEspecialidadDto>> Get(string id)
        {
            var dentistaEspecialidad = await _svDentistaEspecialidad.GetByIdAsync(id);
            if (dentistaEspecialidad == null)
            {
                return NotFound();
            }
            return Ok(dentistaEspecialidad);
        }

        // POST api/<Dentista_EspecialidadController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] DentistaEspecialidadDto dentistaEspecialidadDto)
        {
            if (dentistaEspecialidadDto == null)
            {
                return BadRequest();
            }

            await _svDentistaEspecialidad.AddAsync(dentistaEspecialidadDto);
            await _svDentistaEspecialidad.SaveChangesAsync();

            return CreatedAtAction(nameof(Get), new { id = dentistaEspecialidadDto.ID_Dentista_Especialidad }, dentistaEspecialidadDto);
        }

        // PUT api/<Dentista_EspecialidadController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] DentistaEspecialidadDto dentistaEspecialidadDto)
        {
            if (dentistaEspecialidadDto == null || id != dentistaEspecialidadDto.ID_Dentista_Especialidad)
            {
                return BadRequest();
            }

            await _svDentistaEspecialidad.UpdateAsync(dentistaEspecialidadDto);
            await _svDentistaEspecialidad.SaveChangesAsync();

            return NoContent();
        }

        // DELETE api/<Dentista_EspecialidadController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            var dentistaEspecialidad = await _svDentistaEspecialidad.GetByIdAsync(id);
            if (dentistaEspecialidad == null)
            {
                return NotFound();
            }

            await _svDentistaEspecialidad.DeleteAsync(id);
            await _svDentistaEspecialidad.SaveChangesAsync();

            return NoContent();
        }
    }
}

