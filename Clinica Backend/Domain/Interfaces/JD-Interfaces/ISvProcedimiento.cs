using Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvProcedimiento
    {
        Task<ProcedimientoDto> GetProcedimientoByIdAsync(string idProcedimiento);
        Task<IEnumerable<ProcedimientoDto>> GetAllProcedimientosAsync();
        Task RegisterProcedimientoAsync(ProcedimientoDto procedimientoDto);
        Task UpdateProcedimientoAsync(ProcedimientoDto procedimientoDto);
        Task DeleteProcedimientoAsync(string idProcedimiento);
    }
}
