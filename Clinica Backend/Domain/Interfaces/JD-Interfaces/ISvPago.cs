using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvPago : ISvGeneric<PagoDto>
    {
        Task<PagoDto> GetPagoByIdAsync(Guid idPago);
        Task<IEnumerable<PagoDto>> GetAllPagosAsync();
        Task RegisterPagoAsync(PagoDto pagoDto);
        Task UpdatePagoAsync(PagoDto pagoDto);
        Task DeletePagoAsync(Guid idPago);
    }
}
