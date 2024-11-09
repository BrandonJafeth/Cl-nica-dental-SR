using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvHistorialTratamiento : ISvGeneric<HistorialTratamientoDto>
    {
        Task<HistorialTratamientoDto> GetHistorialTratamientoByIdAsync(string idHistorialTratamiento);
        Task<IEnumerable<HistorialTratamientoDto>> GetAllHistorialesTratamientosAsync();
        Task RegisterHistorialTratamientoAsync(HistorialTratamientoDto historialTratamientoDto);
        Task UpdateHistorialTratamientoAsync(HistorialTratamientoDto historialTratamientoDto);
        Task DeleteHistorialTratamientoAsync(string idHistorialTratamiento);
    }
}
