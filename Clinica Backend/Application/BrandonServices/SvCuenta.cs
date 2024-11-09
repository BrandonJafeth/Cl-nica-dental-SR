using Application.Dtos.PostDtos;
using Clinica_Dental;
using Domain.Interfaces.Brandon_Interfaces;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.BrandonServices
{
    public class SvCuenta : ISvCuenta
    {
        private readonly ISvGeneric<Cuenta> _genericRepository;

        public SvCuenta(ISvGeneric<Cuenta> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<CuentaDto>> GetAllAsync()
        {
            var cuentas = await _genericRepository.GetAllAsync();
            var cuentaDtos = new List<CuentaDto>();

            foreach (var cuenta in cuentas)
            {
                cuentaDtos.Add(MapToDto(cuenta));
            }

            return cuentaDtos;
        }

        public async Task<CuentaDto> GetByIdAsync(string id)
        {
            var cuenta = await _genericRepository.GetByIdAsync(id);
            return MapToDto(cuenta);
        }

        public async Task AddAsync(CuentaDto entity)
        {
            var cuenta = MapToEntity(entity);
            await _genericRepository.AddAsync(cuenta);
        }

        public async Task UpdateAsync(CuentaDto entity)
        {
            var cuenta = MapToEntity(entity);
            await _genericRepository.UpdateAsync(cuenta);
        }

        public async Task DeleteAsync(string id)
        {
            await _genericRepository.DeleteAsync(id);
        }

        public async Task SaveChangesAsync()
        {
            await _genericRepository.SaveChangesAsync();
        }

        private CuentaDto MapToDto(Cuenta cuenta)
        {
            return new CuentaDto
            {
                ID_Cuenta = cuenta.ID_Cuenta,
                Saldo_Total = cuenta.Saldo_Total ?? 0, // Manejo de valor nulo
                Fecha_Apertura = cuenta.Fecha_Apertura ?? DateOnly.MinValue, // Manejo de valor nulo
                Fecha_Cierre = cuenta.Fecha_Cierre ?? DateOnly.MinValue, // Manejo de valor nulo
                Fecha_Ultima_Actualizacion = cuenta.Fecha_Ultima_Actualizacion ?? DateOnly.MinValue, // Manejo de valor nulo
                Observaciones = cuenta.Observaciones,
                ID_Estado_Cuenta = cuenta.ID_Estado_Cuenta,
                ID_Factura = cuenta.ID_Factura,
                ID_Paciente = cuenta.ID_Paciente
            };
        }


        private Cuenta MapToEntity(CuentaDto cuentaDto)
        {
            return new Cuenta
            {
                ID_Cuenta = cuentaDto.ID_Cuenta,
                Saldo_Total = cuentaDto.Saldo_Total,
                Fecha_Apertura = cuentaDto.Fecha_Apertura,
                Fecha_Cierre = cuentaDto.Fecha_Cierre,
                Fecha_Ultima_Actualizacion = cuentaDto.Fecha_Ultima_Actualizacion,
                Observaciones = cuentaDto.Observaciones,
                ID_Estado_Cuenta = cuentaDto.ID_Estado_Cuenta,
                ID_Factura = cuentaDto.ID_Factura,
                ID_Paciente = cuentaDto.ID_Paciente
            };
        }
    }
}

