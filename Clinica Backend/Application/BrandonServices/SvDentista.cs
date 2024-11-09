using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvDentista : ISvDentista
    {
        private readonly ISvGeneric<Dentista> _genericRepository;

        public SvDentista(ISvGeneric<Dentista> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<DentistaDto>> GetAllAsync()
        {
            var dentistas = await _genericRepository.GetAllAsync();
            var dentistaDtos = new List<DentistaDto>();

            foreach (var dentista in dentistas)
            {
                dentistaDtos.Add(MapToDto(dentista));
            }

            return dentistaDtos;
        }

        public async Task<DentistaDto> GetByIdAsync(string id)
        {
            var dentista = await _genericRepository.GetByIdAsync(id);
            return MapToDto(dentista);
        }

        public async Task AddAsync(DentistaDto entity)
        {
            var dentista = MapToEntity(entity);
            await _genericRepository.AddAsync(dentista);
        }

        public async Task UpdateAsync(DentistaDto entity)
        {
            var dentista = MapToEntity(entity);
            await _genericRepository.UpdateAsync(dentista);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private DentistaDto MapToDto(Dentista dentista)
        {
            return new DentistaDto
            {
                ID_Dentista = dentista.ID_Dentista,
                Nombre_Den = dentista.Nombre_Den,
                Apellido1_Den = dentista.Apellido1_Den,
                Apellido2_Den = dentista.Apellido2_Den,
                Direccion_Den = dentista.Direccion_Den,
                FechaNacimiento_Den = dentista.FechaNacimiento_Den ?? DateOnly.MinValue,
                Telefono_Den = dentista.Telefono_Den,
                Correo_Den = dentista.Correo_Den,
                ID_Funcionario = dentista.ID_Funcionario
            };
        }

        private Dentista MapToEntity(DentistaDto dentistaDto)
        {
            return new Dentista
            {
                ID_Dentista = dentistaDto.ID_Dentista,
                Nombre_Den = dentistaDto.Nombre_Den,
                Apellido1_Den = dentistaDto.Apellido1_Den,
                Apellido2_Den = dentistaDto.Apellido2_Den,
                Direccion_Den = dentistaDto.Direccion_Den,
                FechaNacimiento_Den = dentistaDto.FechaNacimiento_Den,
                Telefono_Den = dentistaDto.Telefono_Den,
                Correo_Den = dentistaDto.Correo_Den,
                ID_Funcionario = dentistaDto.ID_Funcionario
            };
        }
    }
}
