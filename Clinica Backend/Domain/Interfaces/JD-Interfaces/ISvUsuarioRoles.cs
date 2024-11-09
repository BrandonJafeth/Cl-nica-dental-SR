using Application.Dtos.PostDtos;
using Domain.Interfaces.Generic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces.JD_Interfaces
{
    public interface ISvUsuarioRoles : ISvGeneric<UsuarioRolesDto>
    {
        Task<UsuarioRolesDto> GetUsuarioRolesByIdAsync(string idUsuarioRoles);
        Task<IEnumerable<UsuarioRolesDto>> GetAllUsuarioRolesAsync();
        Task RegisterUsuarioRolesAsync(UsuarioRolesDto usuarioRolesDto);
        Task UpdateUsuarioRolesAsync(UsuarioRolesDto usuarioRolesDto);
        Task DeleteUsuarioRolesAsync(string idUsuarioRoles);
    }
}
