using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvPaciente : ISvGeneric<PacienteDto>
    {
        Task<PacienteDto> GetPacienteByIdAsync(string idPaciente);
        Task<IEnumerable<PacienteDto>> GetAllPacientesAsync();
        Task RegisterPacienteAsync(PacienteDto pacienteDto);
        Task UpdatePacienteAsync(PacienteDto pacienteDto);
        Task DeletePacienteAsync(string idPaciente);
    }
}
