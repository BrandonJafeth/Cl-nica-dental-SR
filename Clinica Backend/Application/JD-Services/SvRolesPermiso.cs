using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.JD_Interfaces;
using Domain.Interfaces.Generic;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SvRolesPermiso : ISvRolesPermiso, ISvGeneric<RolesPermisosPostDto>
    {
        private readonly MydDbContext _context;

        public SvRolesPermiso(MydDbContext context)
        {
            _context = context;
        }

        public async Task<RolesPermisosPostDto> GetRolesPermisoByIdAsync(string idRolesPermiso)
        {
            var rolesPermiso = await _context.Roles_Permisos.FindAsync(idRolesPermiso);

            if (rolesPermiso == null)
            {
                throw new Exception("RolesPermiso no encontrado");
            }

            return new RolesPermisosPostDto
            {
                ID_Roles_Permisos = rolesPermiso.ID_Roles_Permisos,
                ID_Roles = rolesPermiso.ID_Roles,
                ID_Permisos = rolesPermiso.ID_Permisos
            };
        }

        public async Task<IEnumerable<RolesPermisosPostDto>> GetAllRolesPermisosAsync()
        {
            var rolesPermisos = await _context.Roles_Permisos.ToListAsync();
            return rolesPermisos.Select(rp => new RolesPermisosPostDto
            {
                ID_Roles_Permisos = rp.ID_Roles_Permisos,
                ID_Roles = rp.ID_Roles,
                ID_Permisos = rp.ID_Permisos
            });
        }

        public async Task RegisterRolesPermisoAsync(RolesPermisosPostDto rolesPermisosDto)
        {
            var rolesPermiso = new Roles_Permiso
            {
                ID_Roles_Permisos = rolesPermisosDto.ID_Roles_Permisos,
                ID_Roles = rolesPermisosDto.ID_Roles,
                ID_Permisos = rolesPermisosDto.ID_Permisos
            };

            _context.Roles_Permisos.Add(rolesPermiso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRolesPermisoAsync(RolesPermisosPostDto rolesPermisosDto)
        {
            var rolesPermiso = await _context.Roles_Permisos.FindAsync(rolesPermisosDto.ID_Roles_Permisos);

            if (rolesPermiso == null)
            {
                throw new Exception("RolesPermiso no encontrado");
            }

            rolesPermiso.ID_Roles = rolesPermisosDto.ID_Roles;
            rolesPermiso.ID_Permisos = rolesPermisosDto.ID_Permisos;

            _context.Roles_Permisos.Update(rolesPermiso);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRolesPermisoAsync(string idRolesPermiso)
        {
            var rolesPermiso = await _context.Roles_Permisos.FindAsync(idRolesPermiso);

            if (rolesPermiso == null)
            {
                throw new Exception("RolesPermiso no encontrado");
            }

            _context.Roles_Permisos.Remove(rolesPermiso);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<RolesPermisosPostDto>> GetAllAsync()
        {
            return await GetAllRolesPermisosAsync();
        }

        public async Task<RolesPermisosPostDto> GetByIdAsync(string id)
        {
            return await GetRolesPermisoByIdAsync(id);
        }

        public async Task AddAsync(RolesPermisosPostDto entity)
        {
            await RegisterRolesPermisoAsync(entity);
        }

        public async Task UpdateAsync(RolesPermisosPostDto entity)
        {
            await UpdateRolesPermisoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteRolesPermisoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
