using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Generic;
using Domain.Interfaces.JD_Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Application.Services
{
    public class SvTipoTratamiento : ISvTipoTratamiento, ISvGeneric<TipoTratamientoPostDto>
    {
        private readonly MydDbContext _context;

        public SvTipoTratamiento(MydDbContext context)
        {
            _context = context;
        }

        public async Task<TipoTratamientoPostDto> GetTipoTratamientoByIdAsync(string idTipoTratamiento)
        {
            var tipoTratamiento = await _context.Tipo_Tratamientos.FindAsync(idTipoTratamiento);

            if (tipoTratamiento == null)
            {
                throw new Exception("Tipo de tratamiento no encontrado");
            }

            return new TipoTratamientoPostDto
            {
                ID_TipoTratamiento = tipoTratamiento.ID_TipoTratamiento,
                Nombre_Tipo_Tratamiento = tipoTratamiento.Nombre_Tipo_Tratamiento,
                Descripcion_Tipo_Tratamiento = tipoTratamiento.Descripcion_Tipo_Tratamiento
            };
        }

        public async Task<IEnumerable<TipoTratamientoPostDto>> GetAllTipoTratamientosAsync()
        {
            var tipoTratamientos = await _context.Tipo_Tratamientos.ToListAsync();
            return tipoTratamientos.Select(tt => new TipoTratamientoPostDto
            {
                ID_TipoTratamiento = tt.ID_TipoTratamiento,
                Nombre_Tipo_Tratamiento = tt.Nombre_Tipo_Tratamiento,
                Descripcion_Tipo_Tratamiento = tt.Descripcion_Tipo_Tratamiento
            });
        }

        public async Task RegisterTipoTratamientoAsync(TipoTratamientoPostDto tipoTratamientoDto)
        {
            var tipoTratamiento = new Tipo_Tratamiento
            {
                ID_TipoTratamiento = tipoTratamientoDto.ID_TipoTratamiento,
                Nombre_Tipo_Tratamiento = tipoTratamientoDto.Nombre_Tipo_Tratamiento,
                Descripcion_Tipo_Tratamiento = tipoTratamientoDto.Descripcion_Tipo_Tratamiento
            };

            _context.Tipo_Tratamientos.Add(tipoTratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateTipoTratamientoAsync(TipoTratamientoPostDto tipoTratamientoDto)
        {
            var tipoTratamiento = await _context.Tipo_Tratamientos.FindAsync(tipoTratamientoDto.ID_TipoTratamiento);

            if (tipoTratamiento == null)
            {
                throw new Exception("Tipo de tratamiento no encontrado");
            }

            tipoTratamiento.Nombre_Tipo_Tratamiento = tipoTratamientoDto.Nombre_Tipo_Tratamiento;
            tipoTratamiento.Descripcion_Tipo_Tratamiento = tipoTratamientoDto.Descripcion_Tipo_Tratamiento;

            _context.Tipo_Tratamientos.Update(tipoTratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteTipoTratamientoAsync(string idTipoTratamiento)
        {
            var tipoTratamiento = await _context.Tipo_Tratamientos.FindAsync(idTipoTratamiento);

            if (tipoTratamiento == null)
            {
                throw new Exception("Tipo de tratamiento no encontrado");
            }

            _context.Tipo_Tratamientos.Remove(tipoTratamiento);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<TipoTratamientoPostDto>> GetAllAsync()
        {
            return await GetAllTipoTratamientosAsync();
        }

        public async Task<TipoTratamientoPostDto> GetByIdAsync(string id)
        {
            return await GetTipoTratamientoByIdAsync(id);
        }

        public async Task AddAsync(TipoTratamientoPostDto entity)
        {
            await RegisterTipoTratamientoAsync(entity);
        }

        public async Task UpdateAsync(TipoTratamientoPostDto entity)
        {
            await UpdateTipoTratamientoAsync(entity);
        }

        public async Task DeleteAsync(string id)
        {
            await DeleteTipoTratamientoAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
