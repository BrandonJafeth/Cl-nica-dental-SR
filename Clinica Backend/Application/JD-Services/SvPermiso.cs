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
    public class SvPermiso : ISvPermiso, ISvGeneric<PermisosPostDto>
    {
        private readonly MydDbContext _context;

        public SvPermiso(MydDbContext context)
        {
            _context = context;
        }

        public async Task<PermisosPostDto> GetPermisoByIdAsync(string idPermiso)
        {
            var permiso = await _context.Permisos.FindAsync(idPermiso);

            if (permiso == null)
            {
                throw new Exception("Permiso no encontrado");
            }

            return new PermisosPostDto
            {
                ID_Permisos = permiso.ID_Permisos,
                Nombre = permiso.Nombre,
                Descripcion = permiso.Descripcion
            };
        }

        public async Task<IEnumerable<PermisosPostDto>> GetAllPermisosAsync()
        {
            var permisos = await _context.Permisos.ToListAsync();
            return permisos.Select(p => new PermisosPostDto
            {
                ID_Permisos = p.ID_Permisos,
                Nombre = p.Nombre,
                Descripcion = p.Descripcion
            });
        }

        public async Task RegisterPermisoAsync(PermisosPostDto permisoDto)
        {
            var permiso = new Permiso
            {
                ID_Permisos = permisoDto.ID_Permisos,
                Nombre = permisoDto.Nombre,
                Descripcion = permisoDto.Descripcion
            };

            _context.Permisos.Add(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task UpdatePermisoAsync(PermisosPostDto permisoDto)
        {
            var permiso = await _context.Permisos.FindAsync(permisoDto.ID_Permisos);

            if (permiso == null)
            {
                throw new Exception("Permiso no encontrado");
            }

            permiso.Nombre = permisoDto.Nombre;
            permiso.Descripcion = permisoDto.Descripcion;

            _context.Permisos.Update(permiso);
            await _context.SaveChangesAsync();
        }

        public async Task DeletePermisoAsync(string idPermiso)
        {
            var permiso = await _context.Permisos.FindAsync(idPermiso);

            if (permiso == null)
            {
                throw new Exception("Permiso no encontrado");
            }

            _context.Permisos.Remove(permiso);
            await _context.SaveChangesAsync();
        }

        // Implementación de ISvGeneric
        public async Task<IEnumerable<PermisosPostDto>> GetAllAsync()
        {
            return await GetAllPermisosAsync();
        }

        public async Task<PermisosPostDto> GetByIdAsync(string id)
        {
            return await GetPermisoByIdAsync(id);
        }

        public async Task AddAsync(PermisosPostDto entity)
        {
            await RegisterPermisoAsync(entity);
        }

        public async Task UpdateAsync(PermisosPostDto entity)
        {
            await UpdatePermisoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeletePermisoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
