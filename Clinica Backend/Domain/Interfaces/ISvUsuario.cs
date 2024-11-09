using Application.Dtos.PostDtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ISvUsuario
    {

        void Create(UsuariosDto usuario);
    }
}
