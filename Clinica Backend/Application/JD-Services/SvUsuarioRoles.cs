using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SvUsuarioRoles : ISvUsuarioRoles
    {
        private readonly MydDbContext _context;

        public SvUsuarioRoles(MydDbContext context)
        {
            _context = context;
        }

        public async Task<UsuarioRolesDto> GetUsuarioRolesByIdAsync(string idUsuarioRoles)
        {
            var usuarioRole = await _context.Usuario_Roles.FindAsync(idUsuarioRoles);

            if (usuarioRole == null)
            {
                throw new Exception("Usuario_Role no encontrado");
            }

            return new UsuarioRolesDto
            {
                ID_Usuario_Roles = usuarioRole.ID_Usuario_Roles,
                ID_Usuario = usuarioRole.ID_Usuario,
                ID_Roles = usuarioRole.ID_Roles
            };
        }

        public async Task<IEnumerable<UsuarioRolesDto>> GetAllUsuarioRolesAsync()
        {
            var usuarioRoles = await _context.Usuario_Roles.ToListAsync();
            return usuarioRoles.Select(ur => new UsuarioRolesDto
            {
                ID_Usuario_Roles = ur.ID_Usuario_Roles,
                ID_Usuario = ur.ID_Usuario,
                ID_Roles = ur.ID_Roles
            });
        }

        public async Task RegisterUsuarioRolesAsync(UsuarioRolesDto usuarioRolesDto)
        {
            var usuarioRole = new Usuario_Role
            {
                ID_Usuario_Roles = usuarioRolesDto.ID_Usuario_Roles,
                ID_Usuario = usuarioRolesDto.ID_Usuario,
                ID_Roles = usuarioRolesDto.ID_Roles
            };

            _context.Usuario_Roles.Add(usuarioRole);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateUsuarioRolesAsync(UsuarioRolesDto usuarioRolesDto)
        {
            var usuarioRole = await _context.Usuario_Roles.FindAsync(usuarioRolesDto.ID_Usuario_Roles);

            if (usuarioRole == null)
            {
                throw new Exception("Usuario_Role no encontrado");
            }

            usuarioRole.ID_Usuario = usuarioRolesDto.ID_Usuario;
            usuarioRole.ID_Roles = usuarioRolesDto.ID_Roles;

            _context.Usuario_Roles.Update(usuarioRole);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteUsuarioRolesAsync(string idUsuarioRoles)
        {
            var usuarioRole = await _context.Usuario_Roles.FindAsync(idUsuarioRoles);

            if (usuarioRole == null)
            {
                throw new Exception("Usuario_Role no encontrado");
            }

            _context.Usuario_Roles.Remove(usuarioRole);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<UsuarioRolesDto>> GetAllAsync()
        {
            return await GetAllUsuarioRolesAsync();
        }

        public async Task<UsuarioRolesDto> GetByIdAsync(string id)
        {
            return await GetUsuarioRolesByIdAsync(id);
        }

        public async Task AddAsync(UsuarioRolesDto entity)
        {
            await RegisterUsuarioRolesAsync(entity);
        }

        public async Task UpdateAsync(UsuarioRolesDto entity)
        {
            await UpdateUsuarioRolesAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteUsuarioRolesAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}

