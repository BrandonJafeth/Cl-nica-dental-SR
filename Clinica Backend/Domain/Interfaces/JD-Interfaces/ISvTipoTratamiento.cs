using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvTipoTratamiento : ISvGeneric<TipoTratamientoPostDto>
    {
        Task<TipoTratamientoPostDto> GetTipoTratamientoByIdAsync(string idTipoTratamiento);
        Task<IEnumerable<TipoTratamientoPostDto>> GetAllTipoTratamientosAsync();
        Task RegisterTipoTratamientoAsync(TipoTratamientoPostDto tipoTratamientoDto);
        Task UpdateTipoTratamientoAsync(TipoTratamientoPostDto tipoTratamientoDto);
        Task DeleteTipoTratamientoAsync(string idTipoTratamiento);
    }
}
