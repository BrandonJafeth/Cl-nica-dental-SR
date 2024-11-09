using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvPermiso : ISvGeneric<PermisosPostDto>
    {
        Task<PermisosPostDto> GetPermisoByIdAsync(string idPermiso);
        Task<IEnumerable<PermisosPostDto>> GetAllPermisosAsync();
        Task RegisterPermisoAsync(PermisosPostDto permisoDto);
        Task UpdatePermisoAsync(PermisosPostDto permisoDto);
        Task DeletePermisoAsync(string idPermiso);
    }
}
