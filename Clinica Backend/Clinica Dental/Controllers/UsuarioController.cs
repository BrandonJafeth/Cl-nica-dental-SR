using Application.Dtos.PostDtos;
using Application.JD_Services;
using Domain.Dtos.OtherDtos;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clinica_Dental.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsuarioController : ControllerBase
    {
        private readonly SvUsuario _svUsuario;

        public UsuarioController(SvUsuario svUsuario)
        {
            _svUsuario = svUsuario;
        }

        // GET: api/<UsuarioController>
        [HttpGet]
        public async Task<ActionResult<IEnumerable<UsuariosDto>>> Get()
        {
            var usuarios = await _svUsuario.GetAllUsersAsync();
            return Ok(usuarios);
        }

        // GET api/<UsuarioController>/5
        [HttpGet("{id}")]
        public async Task<ActionResult<UsuariosDto>> Get(string id)
        {
            var usuario = await _svUsuario.GetUserByIdAsync(id);
            if (usuario == null)
            {
                return NotFound();
            }
            return Ok(usuario);
        }

        // POST api/<UsuarioController>
        [HttpPost]
        public async Task<ActionResult> Post([FromBody] UsuariosDto usuarioDto)
        {
            await _svUsuario.RegisterAsync(usuarioDto);
            return CreatedAtAction(nameof(Get), new { id = usuarioDto.ID_Usuario }, usuarioDto);
        }

        // PUT api/<UsuarioController>/5
        [HttpPut("{id}")]
        public async Task<ActionResult> Put(string id, [FromBody] UsuariosDto usuarioDto)
        {
            if (id != usuarioDto.ID_Usuario)
            {
                return BadRequest();
            }

            await _svUsuario.UpdateUserAsync(usuarioDto);
            return NoContent();
        }

        // DELETE api/<UsuarioController>/5
        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(string id)
        {
            await _svUsuario.DeleteUserAsync(id);
            return NoContent();
        }

        // POST api/<UsuarioController>/login
        [HttpPost("login")]
        public async Task<ActionResult<UsuariosDto>> Login([FromBody] LoginDto loginDto)
        {
            var usuario = await _svUsuario.LoginAsync(loginDto.Email, loginDto.Password);
            return Ok(usuario);
        }

        // POST api/<UsuarioController>/register
        [HttpPost("register")]
        public async Task<ActionResult> Register([FromBody] UsuariosDto usuarioDto)
        {
            await _svUsuario.RegisterAsync(usuarioDto);
            return CreatedAtAction(nameof(Get), new { id = usuarioDto.ID_Usuario }, usuarioDto);
        }
    }
}
