using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvHistorialMedico : ISvGeneric<HistorialMedicoDto>
    {
        Task<HistorialMedicoDto> GetHistorialMedicoByIdAsync(string idHistorialMedico);
        Task<IEnumerable<HistorialMedicoDto>> GetAllHistorialesMedicosAsync();
        Task RegisterHistorialMedicoAsync(HistorialMedicoDto historialMedicoDto);
        Task UpdateHistorialMedicoAsync(HistorialMedicoDto historialMedicoDto);
        Task DeleteHistorialMedicoAsync(string idHistorialMedico);
    }
}
