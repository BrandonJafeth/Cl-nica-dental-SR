using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvDentista_Especialidad : ISvDentista_Especialidad
    {
        private readonly ISvGeneric<Dentista_Especialidad> _genericRepository;

        public SvDentista_Especialidad(ISvGeneric<Dentista_Especialidad> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<DentistaEspecialidadDto>> GetAllAsync()
        {
            var dentistaEspecialidades = await _genericRepository.GetAllAsync();
            var dentistaEspecialidadDtos = new List<DentistaEspecialidadDto>();

            foreach (var dentistaEspecialidad in dentistaEspecialidades)
            {
                dentistaEspecialidadDtos.Add(MapToDto(dentistaEspecialidad));
            }

            return dentistaEspecialidadDtos;
        }

        public async Task<DentistaEspecialidadDto> GetByIdAsync(string id)
        {
            var dentistaEspecialidad = await _genericRepository.GetByIdAsync(id);
            return MapToDto(dentistaEspecialidad);
        }

        public async Task AddAsync(DentistaEspecialidadDto entity)
        {
            var dentistaEspecialidad = MapToEntity(entity);
            await _genericRepository.AddAsync(dentistaEspecialidad);
        }

        public async Task UpdateAsync(DentistaEspecialidadDto entity)
        {
            var dentistaEspecialidad = MapToEntity(entity);
            await _genericRepository.UpdateAsync(dentistaEspecialidad);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private DentistaEspecialidadDto MapToDto(Dentista_Especialidad dentistaEspecialidad)
        {
            return new DentistaEspecialidadDto
            {
                ID_Dentista_Especialidad = dentistaEspecialidad.ID_Dentista_Especialidad,
                ID_Dentista = dentistaEspecialidad.ID_Dentista,
                ID_Especialidad = dentistaEspecialidad.ID_Especialidad
            };
        }

        private Dentista_Especialidad MapToEntity(DentistaEspecialidadDto dentistaEspecialidadDto)
        {
            return new Dentista_Especialidad
            {
                ID_Dentista_Especialidad = dentistaEspecialidadDto.ID_Dentista_Especialidad,
                ID_Dentista = dentistaEspecialidadDto.ID_Dentista,
                ID_Especialidad = dentistaEspecialidadDto.ID_Especialidad
            };
        }
    }
}
