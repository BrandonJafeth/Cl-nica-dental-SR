using Application.Dtos.PostDtos;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.JD_Services
{
    public class SvUsuario : ISvUsuario
    {
        private readonly List<UsuariosDto> _usuarios;

        public SvUsuario()
        {
            _usuarios = new List<UsuariosDto>();
        }

        public async Task<UsuariosDto> LoginAsync(string email, string password)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.Email == email && u.Contraseña == password);

            if (usuario == null)
            {
                throw new Exception("Usuario o contraseña incorrectos");
            }

            // Generar y asignar token (lógica de token no incluida)
            usuario.Token = "token_generado";

            return await Task.FromResult(usuario);
        }

        public async Task RegisterAsync(UsuariosDto usuarioDto)
        {
            if (_usuarios.Any(u => u.Email == usuarioDto.Email))
            {
                throw new Exception("El usuario ya existe");
            }

            _usuarios.Add(usuarioDto);
            await Task.CompletedTask;
        }

        public async Task<UsuariosDto> GetUserByIdAsync(string idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.ID_Usuario == idUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            return await Task.FromResult(usuario);
        }

        public async Task UpdateUserAsync(UsuariosDto usuarioDto)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.ID_Usuario == usuarioDto.ID_Usuario);

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            usuario.Nombre = usuarioDto.Nombre;
            usuario.Apellido1 = usuarioDto.Apellido1;
            usuario.Apellido2 = usuarioDto.Apellido2;
            usuario.Email = usuarioDto.Email;
            usuario.Contraseña = usuarioDto.Contraseña;
            usuario.ID_Funcionario = usuarioDto.ID_Funcionario;

            await Task.CompletedTask;
        }

        public async Task DeleteUserAsync(string idUsuario)
        {
            var usuario = _usuarios.FirstOrDefault(u => u.ID_Usuario == idUsuario);

            if (usuario == null)
            {
                throw new Exception("Usuario no encontrado");
            }

            _usuarios.Remove(usuario);
            await Task.CompletedTask;
        }

        public async Task<IEnumerable<UsuariosDto>> GetAllUsersAsync()
        {
            return await Task.FromResult(_usuarios);
        }
    }
}
