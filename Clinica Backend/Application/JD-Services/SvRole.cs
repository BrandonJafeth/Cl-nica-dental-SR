using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.JD_Services
{
    public class SvRole : ISvRole
    {
        private readonly MydDbContext _context;

        public SvRole(MydDbContext context)
        {
            _context = context;
        }

        public async Task<RolesPostDto> GetRoleByIdAsync(string idRole)
        {
            var role = await _context.Roles.FindAsync(idRole);

            if (role == null)
            {
                throw new Exception("Role no encontrado");
            }

            return new RolesPostDto
            {
                ID_Roles = role.ID_Roles,
                Nombre = role.Nombre,
                Descripcion = role.Descripcion
            };
        }

        public async Task<IEnumerable<RolesPostDto>> GetAllRolesAsync()
        {
            var roles = await _context.Roles.ToListAsync();
            return roles.Select(r => new RolesPostDto
            {
                ID_Roles = r.ID_Roles,
                Nombre = r.Nombre,
                Descripcion = r.Descripcion
            });
        }

        public async Task RegisterRoleAsync(RolesPostDto roleDto)
        {
            var role = new Role
            {
                ID_Roles = roleDto.ID_Roles,
                Nombre = roleDto.Nombre,
                Descripcion = roleDto.Descripcion
            };

            _context.Roles.Add(role);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateRoleAsync(RolesPostDto roleDto)
        {
            var role = await _context.Roles.FindAsync(roleDto.ID_Roles);

            if (role == null)
            {
                throw new Exception("Role no encontrado");
            }

            role.Nombre = roleDto.Nombre;
            role.Descripcion = roleDto.Descripcion;

            _context.Roles.Update(role);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteRoleAsync(string idRole)
        {
            var role = await _context.Roles.FindAsync(idRole);

            if (role == null)
            {
                throw new Exception("Role no encontrado");
            }

            _context.Roles.Remove(role);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<RolesPostDto>> GetAllAsync()
        {
            return await GetAllRolesAsync();
        }

        public async Task<RolesPostDto> GetByIdAsync(string id)
        {
            return await GetRoleByIdAsync(id);
        }

        public async Task AddAsync(RolesPostDto entity)
        {
            await RegisterRoleAsync(entity);
        }

        public async Task UpdateAsync(RolesPostDto entity)
        {
            await UpdateRoleAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteRoleAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }

}
